﻿using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public class SystemConfigurationService : ISystemConfigurationService
{
    private readonly IUnitOfWork _unitOfWork;

    public SystemConfigurationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SystemConfiguration> GetSystemConfiguration(string assetTag)
    {
        var sysConfig = new SystemConfiguration
        {
            MachineDetails = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag),
            Motherboards = await _unitOfWork.GetRepository<MotherboardInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync(),
            PhysicalMemories = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync(),
            HardDisks = await _unitOfWork.GetRepository<HardDiskInfo>().Entities
                .Where(x => x.MachineId == assetTag)
                .ToListAsync(),
            Processors = await _unitOfWork.GetRepository<ProcessorInfo>().Entities
                .Where(x => x.MachineId == assetTag)
                .ToListAsync(),
            Mouses = await _unitOfWork.GetRepository<MouseInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync(),
            Keyboards = await _unitOfWork.GetRepository<KeyboardInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync(),
            Monitors = await _unitOfWork.GetRepository<MonitorInfo>().Entities
                .Where(x => x.MachineId == assetTag).ToListAsync()
        };
        return sysConfig;
    }

    public async Task<IResult> AddSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        try
        {
            var assetTag = systemConfiguration.MachineDetails.AssetTag;
            if (await _unitOfWork.GetRepository<MachineInfo>()
                    .GetByIdAsync(assetTag) is not null)
                throw new Exception("Machine with the same asset tag already present");
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(systemConfiguration.MachineDetails);

            // var existingMotherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
            //     .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            // if (existingMotherboard is not null)
            // {
            //     if (!string.IsNullOrEmpty(existingMotherboard.MachineId))
            //     {
            //         throw new Exception("The Selected Motherboard is already in use by another machine");
            //     }
            //     else
            //     {
            //         existingMotherboard.MachineId = assetTag;
            //         await _unitOfWork.GetRepository<MotherboardInfo>()
            //             .UpdateAsync(existingMotherboard, existingMotherboard.OemSerialNo);
            //     }
            // }
            // else
            // {
            //     await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(systemConfiguration.Motherboard);
            // }
            //
            // var existingKeyboard = await _unitOfWork.GetRepository<KeyboardInfo>()
            //     .GetByIdAsync(systemConfiguration.Keyboards.OemSerialNo);
            // if (existingKeyboard is not null)
            // {
            //     if (!string.IsNullOrEmpty(existingKeyboard.MachineId))
            //     {
            //         throw new Exception("The Selected Keyboard is already in use by another machine");
            //     }
            //     else
            //     {
            //         existingKeyboard.MachineId = assetTag;
            //         await _unitOfWork.GetRepository<KeyboardInfo>()
            //             .UpdateAsync(existingKeyboard, existingKeyboard.OemSerialNo);
            //     }
            // }
            // else
            // {
            //     await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(systemConfiguration.Keyboards);
            // }
            //
            // var existingMouse = await _unitOfWork.GetRepository<MouseInfo>()
            //     .GetByIdAsync(systemConfiguration.Mouses.OemSerialNo);
            // if (existingMouse is not null)
            // {
            //     if (!string.IsNullOrEmpty(existingMouse.MachineId))
            //     {
            //         throw new Exception("The Selected Mouse is already in use by another machine");
            //     }
            //     else
            //     {
            //         existingMouse.MachineId = assetTag;
            //         await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(existingMouse, existingMouse.OemSerialNo);
            //     }
            // }
            // else
            // {
            //     await _unitOfWork.GetRepository<MouseInfo>().AddAsync(systemConfiguration.Mouses);
            // }

            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine added successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EditSystemConfiguration(SystemConfiguration systemConfiguration)
    {
        try
        {
            // var assetTag = systemConfiguration.MachineDetails.AssetTag;
            // if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag) is null)
            //     throw new Exception("Machine does not exist in database");
            // await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(systemConfiguration.MachineDetails, assetTag);
            //
            // var motherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
            //     .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            // if (motherboard is not null && string.IsNullOrEmpty(motherboard.MachineId))
            //     throw new Exception("The Selected Motherboard is already in use by another machine");
            // //await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync()
            //
            // var existingOldMotherboard = await _unitOfWork.GetRepository<MotherboardInfo>()
            //     .GetByIdAsync(systemConfiguration.Motherboard.OemSerialNo);
            // if (existingOldMotherboard is not null)
            //     existingOldMotherboard.MachineId = assetTag;


            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Machine updated successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }
}