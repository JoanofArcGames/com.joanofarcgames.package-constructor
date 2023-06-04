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
	    public string version = "0.0.1-pre.1";
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
	    [Header("Delete redundant directories")]
	    [Space(7)]
	    
	    [Indent(1)]
	    [DisplayName("/Editor/")]
	    public bool deleteEditor;
	    [Indent(1)]
	    [DisplayName("/Runtime/")]
	    public bool deleteRuntime;
	    [Indent(1)]
	    [DisplayName("/Tests/")]
	    public bool deleteTests;

	    [Indent(2)]
	    [ShowIf("deleteTests", true)]
	    [DisplayName("/Tests/Editor/")]
	    public bool deleteTestsEditor;
	    [Indent(2)]
	    [ShowIf("deleteTests", true)]
	    [DisplayName("/Tests/Runtime/")]
	    public bool deleteTestsRuntime;

	    [Space(10)]
	    [Header("Cleanup")]
	    [Space(7)]
	    
	    [Indent(1)]
	    [DisplayName("Delete Convertor on complete")]
	    public bool deleteSelf;
	    [Indent(1)]
		[DisplayName("Delete SampleScript.cs files")]
	    public bool deleteSampleScripts;

	    [Space(30)]
	    
	    [HelpBox("Caution: don't put any valuable files inside package root directory and any of the subdirectories prior to conversion. They will be lost.", MessageType.Warning)]
	    [ButtonInvoke(typeof(ConstructionSystem), nameof(ConstructionSystem.Construct), null, typeof(ConfigSO), nameof(GetConfigData))]
	    public bool convert;

	    private bool deleteTestsCurrentState;

	    private void Awake()
	    {
		    deleteTestsCurrentState = deleteTests;
	    }

	    private void OnValidate()
	    {
		    if (deleteTests != deleteTestsCurrentState)
		    {
			    if (deleteTests)
			    {
				    deleteTestsEditor = true;
				    deleteTestsRuntime = true;
			    }
			    else
			    {
				    deleteTestsEditor = false;
				    deleteTestsRuntime = false;
			    }
		    }
		    
		    if (deleteTestsEditor && deleteTestsRuntime)
            {
                deleteTests = true;
            }
		    
		    deleteTestsCurrentState = deleteTests;
	    }

	    public ConfigData GetConfigData()
	    {
		    return new ConfigData
		    {
			    
		    };
	    }
    }
}
#endif