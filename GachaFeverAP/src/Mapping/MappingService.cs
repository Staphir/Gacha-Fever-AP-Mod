using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GachaFeverAP.Mapping.Json;
using GachaFeverAP.Mapping.Models;
using Newtonsoft.Json;

namespace GachaFeverAP.Mapping;

public sealed class MappingService
{
    private MappingService(
        Dictionary<string, ItemMappingEntry> itemsByName,
        Dictionary<long, ItemMappingEntry> itemsById,
        Dictionary<string, LocationMappingEntry> locationsByName,
        Dictionary<long, LocationMappingEntry> locationsById,
        Dictionary<string, VictoryMappingEntry> victoriesByName,
        Dictionary<long, VictoryMappingEntry> victoriesById)
    {
        ItemsByName = itemsByName;
        ItemsById = itemsById;
        LocationsByName = locationsByName;
        LocationsById = locationsById;
        VictoriesByName = victoriesByName;
        VictoriesById = victoriesById;
    }

    public IReadOnlyDictionary<string, ItemMappingEntry> ItemsByName { get; }
    public IReadOnlyDictionary<long, ItemMappingEntry> ItemsById { get; }
    public IReadOnlyDictionary<string, LocationMappingEntry> LocationsByName { get; }
    public IReadOnlyDictionary<long, LocationMappingEntry> LocationsById { get; }
    public IReadOnlyDictionary<string, VictoryMappingEntry> VictoriesByName { get; }
    public IReadOnlyDictionary<long, VictoryMappingEntry> VictoriesById { get; }

    public static MappingService Load(string folder)
    {
        var itemsPath = Path.Combine(folder, "items.json");
        var locationsPath = Path.Combine(folder, "locations.json");
        var victoriesPath = Path.Combine(folder, "victories.json");

        var itemsJson = File.ReadAllText(itemsPath);
        var locationsJson = File.ReadAllText(locationsPath);
        var victoriesJson = File.ReadAllText(victoriesPath);

        var items = JsonConvert.DeserializeObject<ItemsWrapper>(itemsJson);
        var locations = JsonConvert.DeserializeObject<LocationsWrapper>(locationsJson);
        var victories = JsonConvert.DeserializeObject<VictoriesWrapper>(victoriesJson);

        var itemsByName = items.items.ToDictionary(x => x.name, StringComparer.OrdinalIgnoreCase);
        var itemsById = items.items.ToDictionary(x => x.id);

        var locationsByName = locations.locations.ToDictionary(x => x.name, StringComparer.OrdinalIgnoreCase);
        var locationsById = locations.locations.ToDictionary(x => x.id);

        var victoriesByName = victories.victory_items.ToDictionary(x => x.name, StringComparer.OrdinalIgnoreCase);
        var victoriesById = victories.victory_items.ToDictionary(x => x.id);

        return new MappingService(
            itemsByName,
            itemsById,
            locationsByName,
            locationsById,
            victoriesByName,
            victoriesById
        );
    }

    public bool TryGetItemById(long id, out ItemMappingEntry entry)
    {
        return ItemsById.TryGetValue(id, out entry);
    }

    public bool TryGetItemByName(string name, out ItemMappingEntry entry)
    {
        return ItemsByName.TryGetValue(name, out entry);
    }

    public bool TryGetLocationById(long id, out LocationMappingEntry entry)
    {
        return LocationsById.TryGetValue(id, out entry);
    }

    public bool TryGetLocationByName(string name, out LocationMappingEntry entry)
    {
        return LocationsByName.TryGetValue(name, out entry);
    }

    public bool TryGetVictoryById(long id, out VictoryMappingEntry entry)
    {
        return VictoriesById.TryGetValue(id, out entry);
    }

    public bool TryGetVictoryByName(string name, out VictoryMappingEntry entry)
    {
        return VictoriesByName.TryGetValue(name, out entry);
    }
}