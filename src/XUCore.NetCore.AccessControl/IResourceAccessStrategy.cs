﻿
using Microsoft.AspNetCore.Mvc;

namespace XUCore.NetCore.AccessControl
{
    public interface IResourceAccessStrategy
    {
        /// <summary>
        /// Is resource can be accessed
        /// </summary>
        /// <param name="accessKey">accessKey</param>
        /// <returns></returns>
        bool IsCanAccess(string accessKey);

        /// <summary>
        /// AccessStrategyName
        /// </summary>
        //string StrategyName { get; }

        IActionResult DisallowedCommonResult { get; }

        IActionResult DisallowedAjaxResult { get; }
    }
}
