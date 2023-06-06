#if UNITY_EDITOR
using System;

namespace JoanofArcGames.PackageConstructor
{
	public static class ManifestSerializationSystem
    {
	    public static string GetJsonString(ManifestData data)
	    {
		    const string c = ",";
		    string output = "";
		    string nl = Environment.NewLine;

		    output += "{" + nl;
		    
		    if (data.name != "") output += $"\t\"name\": \"{data.name}\"";
		    if (data.version != "")	output += c + nl + $"\t\"version\": \"{data.version}\"";
		    if (data.displayName != "")	output += c + nl + $"\t\"displayName\": \"{data.displayName}\"";
		    if (data.description != "")	output += c + nl + $"\t\"description\": \"{data.description}\"";
		    if (data.unity != "") output += c + nl + $"\t\"unity\": \"{data.unity}\"" + nl;
		    if (data.unityRelease != "") output += c + nl + $"\t\"unityRelease\": \"{data.unityRelease}\"";
		    if (data.documentationUrl != "") output += c + nl + $"\t\"documentationUrl\": \"{data.documentationUrl}\"";
		    if (data.changelogUrl != "") output += c + nl + $"\t\"changelogUrl\": \"{data.changelogUrl}\"";
		    if (data.licensesUrl != "")	output += c + nl + $"\t\"licensesUrl\": \"{data.licensesUrl}\"";
		    if (data.license != "") output += c + nl + $"\t\"license\": \"{data.license}\"";
		    if (data.hideInEditor == false) output += c + nl + "\t\"hideInEditor\": false";
		    
		    if (data.dependencies != null && data.dependencies.Length != 0)
		    {
			    bool hasValidEntry = false;
			    foreach (var entry in data.dependencies)
			    {
				    if (entry.name != "" && entry.version != "") hasValidEntry = true;
			    }

			    if (hasValidEntry)
			    {
				    output += c + nl + "\t\"dependencies\": {" + nl;
                    int counter = 1;
                    int length = data.dependencies.Length;
                    foreach (var entry in data.dependencies)
                    {
                        string comma = counter == length ? "" : c;
                        if (entry.name != "" && entry.version != "") output += $"\t\t\"{entry.name}\": \"{entry.version}\"" + comma + nl;
                        counter++;
                    }
                    output += "\t}";
			    }
		    }

		    if (data.keywords != null && data.keywords.Length != 0)
		    {
			    bool hasValidEntry = false;
			    foreach (var entry in data.keywords)
			    {
				    if (entry != "") hasValidEntry = true;
			    }

			    if (hasValidEntry)
			    {
				    output += c + nl + "\t\"keywords\": [" + nl;
				    int counter = 1;
				    int length = data.keywords.Length;
				    foreach (string keyword in data.keywords)
				    {
					    string comma = counter == length ? "" : c;
					    if (keyword != "") output += $"\t\t\"{keyword}\"" + comma + nl;
					    counter++;
				    }

				    output += "\t]";
			    }
		    }

		    if (data.author.name != "" || data.author.email != "" || data.author.url != "")
		    {
			    bool previous = false;
			    output += c + nl + "\t\"author\": {" + nl;
			    if (data.author.name != "")
			    {
				    output += $"\t\t\"name\": \"{data.author.name}\"";
				    previous = true;
			    }

			    if (data.author.email != "")
			    {
				    if (previous) output += c + nl;
				    output += $"\t\t\"email\": \"{data.author.email}\"";
				    previous = true;
			    }

			    if (data.author.url != "")
			    {
				    if (previous) output += c + nl;
				    output += $"\t\t\"url\": \"{data.author.url}\"";
			    }
			    output += nl + "\t}";
		    }

		    output += nl + "}";

		    return output;
	    }
    }
}
#endif