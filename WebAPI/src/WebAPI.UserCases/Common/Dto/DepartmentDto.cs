﻿using System;
using WebAPI.Entities.Models;
using WebAPI.UserCases.Common.Mappings;

namespace WebAPI.UserCases.Common.Dto
{
    /// <summary>
    /// Sets a properties of the data transfer object for department entity.
    /// </summary>
    public class DepartmentDto : IMapFrom<Department>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}