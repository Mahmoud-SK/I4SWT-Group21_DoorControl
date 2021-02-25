namespace I4SWT_Group21_DoorControl
{
	public interface IEntryNotification
	{
		void NotifyEntryGranted(int id);
		void NotifyEntryDenied(int id);
	}
}