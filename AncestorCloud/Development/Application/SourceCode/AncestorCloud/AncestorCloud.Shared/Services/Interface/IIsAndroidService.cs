using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tcc.App.Core.Services
{
    /// <summary>
   	///Should be instantiated on Android ONLY which allows us to determine whether we're running from Android 
    /// in PCL code by calling Mvx.CanResolve<IIsAndroidService>()
    /// 
    /// Do NOT instantiate for Touch!
    /// 
    /// Incidentally we should never need to do this, I'm just hacking my way around some platform specific issues at the moment.
    /// Should be removed later (famous last words)
    /// </summary>
    public interface IIsAndroidService
    {
    }
}
