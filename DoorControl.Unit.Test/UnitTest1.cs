using NUnit.Framework;
using NSubstitute;
using I4SWT_Group21_DoorControl;

namespace DCS.Unit.Test
{
	public class Tests
	{
		private IDoor door_;
		private IEntryNotification entryNotification_;
		private IUserValidation userValidation_;
		private IAlarm alarm_;
		private DoorControl uut_;
		[SetUp]
		public void Setup()
		{
			door_ = Substitute.For<IDoor>();
			entryNotification_ = Substitute.For<IEntryNotification>();
			userValidation_ = Substitute.For<IUserValidation>();
			alarm_ = Substitute.For<IAlarm>();

			uut_ = new DoorControl(door_, entryNotification_, userValidation_, alarm_);
		}

		[Test]
		public void RequestEntry_ValidationDoor_ReturnOK()
		{
			userValidation_.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
			uut_.RequestEntry(15);
			door_.Received(1).Open();
		}

		[Test]
		public void RequestEntry_ValidationNotification_ReturnOK()
		{
			userValidation_.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
			uut_.RequestEntry(15);
			entryNotification_.Received(1).NotifyEntryGranted(15);
		}

		[Test]
		public void DoorOpened_Breached_ReturnAlarm()
		{
			uut_.DoorOpened();
			alarm_.Received(1).RaiseAlarm();
		}


	}
}