using Eclipse.Audio;
using UnityEngine;

public class MaterialMapper<T> : ScriptableObject where T : System.Enum
{
    [System.Serializable]
    public class MaterialMapping
    {
        [SerializeField] private Material _material;
        [SerializeField] private T _element;

        public Material Material => _material;
        public T Element => _element; 
    }

    [SerializeField] private MaterialMapping[] _materialMappings;

    public T GetElement(Material materialToCheck)
    {
        foreach (var mapping in _materialMappings)
        {
            if (mapping.Material.name == materialToCheck.name.Replace(" (Instance)", ""))
                return mapping.Element;
        }

        Debug.LogWarning($"No audio clip found for material: {materialToCheck.name}");
        return default; // enums can't be null, default returns first enum value
    }
}

//[CreateAssetMenu(fileName = "MaterialMapperDark", menuName = "Eclipse/Audio/MaterialMapperDark")]
//public class MaterialMapperDark : MaterialMapper<DarkElements> { }

//[CreateAssetMenu(fileName = "MaterialMapperLight", menuName = "Eclipse/Audio/MaterialMapperLight")]
//public class MaterialMapperLight : MaterialMapper<LightElements> { }