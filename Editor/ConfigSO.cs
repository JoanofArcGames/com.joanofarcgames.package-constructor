#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	[CreateAssetMenu(fileName = "NewConfig", menuName = "JAG/Construction config")]
	public class ConfigSO : ScriptableObject
    {
        [Header("Package information")]
	    [Space(7)]
	    
	    [HelpBox("* - required fields", MessageType.Info)]
	    
	    [Indent(1)]
	    [DisplayName("Version*")]
	    public string version = "0.1.0-pre.1";
	    [Indent(1)]
	    [DisplayName("Company Name*")]
	    public string companyName = "SampleCompany";
	    [Indent(1)]
	    [DisplayName("Package Name*")]
	    public string packageName = "SamplePackage";
	    [Indent(1)]
	    public string displayName = "";
	    [Indent(1)]
	    public string description = "";
	    [Indent(1)]
	    public string unityVersion = "";
	    [Indent(1)]
	    public string unityRelease = "";
	    [Indent(1)]
	    [DisplayName("Documentation URL")]
	    public string docsUrl = "";
	    [Indent(1)]
	    [DisplayName("Changelog URL")]
	    public string changelogUrl = "";
	    [Indent(1)]
	    [DisplayName("Licenses URL")]
	    public string licensesUrl = "";
	    [Indent(1)]
	    public string license;
	    [Indent(1)]
	    public bool hideInEditor;

	    [Space(10)]
	    
	    public Dependency[] dependencies;

	    [Space(10)]
	    
	    public string[] keywords;

	    [Space(10)]
	    [Header("Author")]
	    [Space(7)]
	    
	    [Indent(1)]
	    [DisplayName("Name")]
	    public string authorName = "";
	    [Indent(1)]
	    [DisplayName("Email")]
	    public string authorEmail = "";
	    [Indent(1)]
	    [DisplayName("URL")]
	    public string authorUrl = "";
	    
	    [Space(10)]
	    [Header("Directories")]
	    [Space(7)]
	    
	    [Indent(1)]
	    [DisplayName("/Editor/")]
	    public bool editor;
	    [Indent(1)]
	    [DisplayName("/Runtime/")]
	    public bool runtime;
	    [Indent(1)]
	    [DisplayName("/Tests/Editor/")]
	    public bool testsEditor;
	    [Indent(1)]
	    [DisplayName("/Tests/Runtime/")]
	    public bool testsRuntime;

	    [Space(30)]
	    
	    [ButtonInvoke(typeof(ConstructionSystem), nameof(ConstructionSystem.Construct), null, typeof(ConfigSO), nameof(GetConfigData))]
	    public bool construct;

	    public ConfigData GetConfigData()
	    {
		    return new ConfigData
		    {
			    
		    };
	    }
    }
}
#endif