using System;

namespace AncestorCloud.Shared
{
	public class ACCommand : System.Windows.Input.ICommand
	{
		private Action _action;

		public ACCommand(Action action)
		{
			_action = action;
		}

		public void Execute(object o)
		{
			_action ();
		}

		public bool CanExecute(object o)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

	}
}

