using System.Collections.Generic;
using GachaFeverAP.Mapping.Models;

namespace GachaFeverAP.Mapping.Json;

public class ItemsWrapper
{
    public List<ItemMappingEntry> items { get; set; }
}