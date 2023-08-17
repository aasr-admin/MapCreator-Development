using Microsoft.Win32.SafeHandles;

namespace UltimaSDK
{
	public class ClientWindowHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		public static ClientWindowHandle Invalid = new(new IntPtr(-1));

		public ClientWindowHandle()
		{
		}

		public ClientWindowHandle(IntPtr value)
		{
			handle = value;
		}

		protected override bool ReleaseHandle()
		{
			if (!IsClosed)
			{
				return ReleaseHandle();
			}

			return true;
		}
	}

	public class ClientProcessHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		public static ClientProcessHandle Invalid = new(new IntPtr(-1));

		public ClientProcessHandle()
			: base()
		{
		}

		public ClientProcessHandle(IntPtr value)
			: base()
		{
			handle = value;
		}

		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseHandle(this) == 0;
		}
	}
}
