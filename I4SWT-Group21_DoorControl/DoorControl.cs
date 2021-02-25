using System;
using System.Collections.Generic;
using System.Text;

namespace I4SWT_Group21_DoorControl
{
	enum State
	{
		DoorClosed,
		DoorOpening,
		DoorClosing,
		DoorBreached
	}
	
	public class DoorControl
	{
		private State currentState;
		private IDoor door_;
		private IEntryNotification entryNotification_;
		private IUserValidation userValidation_;
		private IAlarm alarm_;

		public DoorControl(IDoor door, IEntryNotification entryNotification, IUserValidation userValidation, IAlarm alarm)
		{
			door_ = door;
			entryNotification_ = entryNotification;
			userValidation_ = userValidation;
			alarm_ = alarm;
			currentState = State.DoorClosed;
		}

		public void RequestEntry(int id)
		{
			if (userValidation_.ValidateEntryRequest(id))
			{
				door_.Open();
				entryNotification_.NotifyEntryGranted(id);
				currentState = State.DoorOpening;
			}
			else
			{
				entryNotification_.NotifyEntryDenied(id);	
			}

		}

		public void DoorOpened()
		{
			door_.Close();

			if (currentState == State.DoorClosed)
			{
				alarm_.RaiseAlarm();
				currentState = State.DoorBreached;
			}
			else
			{
				currentState = State.DoorClosing;
			}
		}
		public void DoorClosed()
		{
			currentState = State.DoorClosed;
		}
	}
}
