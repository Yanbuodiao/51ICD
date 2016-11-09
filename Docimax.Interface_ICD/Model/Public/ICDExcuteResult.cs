﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Model
{
    /// <summary>
    /// 执行结果类
    /// </summary>
    public class ICDExcuteResult<T> : ExcuteResult
    {
        public T TResult { get; set; }
    }

    public class ExcuteResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 出现错误时，错误提示
        /// </summary>
        public string ErrorStr { get; set; }
    }
}
