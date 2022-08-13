﻿namespace Fluid.Shared.Entities;

public class MonitorInfo : IEntity
{
    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public MonitorPanelType PanelType { get; set; }
    public decimal RefreshRate { get; set; }
    public bool HasBuiltInSpeakers { get; set; }
    public int HDMIPortCount { get; set; }
    public int VGAPortCount { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Monitor;
}