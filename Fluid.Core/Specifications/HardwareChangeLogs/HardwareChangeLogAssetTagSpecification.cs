﻿using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogAssetTagSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogAssetTagSpecification(string assetTag)
    {
        if (string.IsNullOrEmpty(assetTag))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetTag = assetTag.ToLower();
            FilterCondition = p => p.AssetTag.ToLower().Contains(assetTag);
        }
    }
}