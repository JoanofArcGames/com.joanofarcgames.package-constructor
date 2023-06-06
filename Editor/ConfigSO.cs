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
        [Tooltip("Should be SemVer compatible")]
	    public string version = "0.1.0-pre.1";
	    [Indent(1)]
	    [DisplayName("Company Name*")]
	    [Tooltip("Should be PascalCase")]
	    public string companyName = "SampleCompany";
	    [Indent(1)]
	    [DisplayName("Package Name*")]
	    [Tooltip("Should be PascalCase")]
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
	    public string license = "";
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
	    public bool runtime = true;
	    [Indent(1)]
	    [DisplayName("/Tests/Editor/")]
	    public bool testsEditor;
	    [Indent(1)]
	    [DisplayName("/Tests/Runtime/")]
	    public bool testsRuntime = true;
	    [Indent(1)]
	    [DisplayName("/Samples~/")]
	    public bool samples;
	    [Indent(1)]
	    [DisplayName("/Documentation~/")]
	    public bool documentation = true;

	    [Space(10)]
	    [Header("Markdown files")]
	    [Space(7)]

	    [Indent(1)]
	    [DisplayName("README.md")]
	    public bool readmeMD = true;
	    [Indent(1)]
	    [DisplayName("CHANGELOG.md")]
	    public bool changelogMD = true;
	    [Indent(1)]
	    [DisplayName("LICENSE.md")]
	    public bool licenseMD = true;
	    [Indent(1)]
	    [DisplayName("Third Party Notices.md")]
	    public bool thirdpartyMD;
	    
	    [Space(10)]
	    [Header("Miscellaneous")]
	    [Space(7)]
	    
		[ShowIf(nameof(documentation))]
	    [Indent(1)]
	    [DisplayName("Include template documentation")]
	    public bool templateDocs = true;
	    [Indent(1)]
	    [DisplayName("Include blank scripts")]
	    public bool blankScripts = true;
	    [Indent(1)]
	    public bool enableLogging;

	    [Space(30)]
	    
	    [ButtonInvoke(typeof(ConstructionSystem), nameof(ConstructionSystem.Construct), null, typeof(ConfigSO), nameof(GetConfigData))]
	    public bool construct;

	    public ConfigData GetConfigData()
	    {
		    return new ConfigData
		    {
			    version = version,
			    companyName = companyName,
			    packageName = packageName,
			    displayName = displayName,
			    description = description,
			    unityVersion = unityVersion,
			    unityRelease = unityRelease,
			    docsUrl = docsUrl,
			    changelogUrl = changelogUrl,
			    licensesUrl = licensesUrl,
			    license = license,
			    hideInEditor = hideInEditor,
			    dependencies = dependencies,
			    keywords = keywords,
			    author = new Author
			    {
				    name = authorName,
				    email = authorEmail,
				    url = authorUrl
			    },
			    editor = editor,
			    runtime = runtime,
			    testsEditor = testsEditor,
			    testsRuntime = testsRuntime,
			    samples = samples,
			    documentation = documentation,
			    templateDocs = templateDocs,
			    blankScripts = blankScripts,
			    readmeMD = readmeMD,
			    changelogMD = changelogMD,
			    licenseMD = licenseMD,
			    thirdpartyMD = thirdpartyMD,
			    enableLogging = enableLogging
		    };
	    }
    }
}
#endif