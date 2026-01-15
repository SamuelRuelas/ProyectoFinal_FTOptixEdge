// Modificacion de HTML para creacion de 4 gauges (1 grupal y 3 individuales)

#region Using directives
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.UI;
using UAManagedCore;
#endregion

public class GaugeChartLogic : BaseNetLogic
{
    public override void Start()
    {
        // Llama inicialmente con un valor por defecto
        RefreshGraph(50, 75, 80);
    }

    public override void Stop()
    {
    }

    [ExportMethod]
    public void RefreshGraph(double val1, double val2, double val3)
    {
        var pathHtml = ResourceUri.FromProjectRelativePath("eCharts/gauge.html");
        var pathJs = ResourceUri.FromProjectRelativePath("eCharts/gaugeData.js");
        bool darkMode = Owner.GetVariable("DarkMode").Value;

        GenerateJavaScriptFile(pathJs.Uri, darkMode, val1, val2, val3);
        GenerateHtmlFile(pathHtml.Uri);

        var browser = (WebBrowser)Owner;
        browser.URL = pathHtml;
        browser.Refresh();

        Log.Info("Gauge URL loaded: " + pathHtml);
    }

    //NOTA: Contar con el archivo echarts.js dentro de la carpeta eCharts en los archivos del proyecto de FTOptix Studio

    private static void GenerateHtmlFile(string fullPath)
    {
        string html = $@"
<!DOCTYPE html>
<html lang=""en"" style=""height: 100%"">
<head>
    <meta charset=""utf-8"">
    <style>
    html, body {{
        margin: 0;
        width: 100%;
        height: 100%;
        overflow: hidden;
    }}
    
    #gauge-container {{
        width: 100%;
        height: 100%;
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        grid-auto-rows: 1fr;
        gap: 0px;
        padding: 0px;
        box-sizing: border-box;
    }}
    
    .gauge-box {{
        width: 100%;
        height: 100%;
    }}
</style>
</head>
<body style=""height: 100%; margin: 0"">
    <div id=""gauge-container"">
        <div id=""g1"" class=""gauge-box""></div>
        <div id=""g2"" class=""gauge-box""></div>
        <div id=""g3"" class=""gauge-box""></div>
        <div id=""g4"" class=""gauge-box""></div>    
    </div>
    <script type=""text/javascript"" src=""./echarts.js""></script>
    <script type=""text/javascript"" src=""./gaugeData.js""></script>
</body>
</html>";

        System.IO.File.WriteAllText(fullPath, html);
    }

    private void GenerateJavaScriptFile(string filePath, bool darkMode, double valor1, double valor2, double valor3)
    {
        string valor1Js = valor1.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string valor2Js = valor2.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string valor3Js = valor3.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string modoOscuro = darkMode ? "'dark'" : "null";

        string js = $@"
document.addEventListener('DOMContentLoaded', function () {{
    var gauge1 = document.getElementById('g1');
    var gauge2 = document.getElementById('g2');
    var gauge3 = document.getElementById('g3');
    var gauge4 = document.getElementById('g4');
    var myChart1 = echarts.init(gauge1, {modoOscuro}, {{ renderer: 'svg' }});
    var myChart2 = echarts.init(gauge2, {modoOscuro}, {{ renderer: 'svg' }});
    var myChart3 = echarts.init(gauge3, {modoOscuro}, {{ renderer: 'svg' }});
    var myChart4 = echarts.init(gauge4, {modoOscuro}, {{ renderer: 'svg' }});

    const gaugeData = [
        {{
            value: {valor1Js},
            name: 'Perfect',
            title: {{
                offsetCenter: ['0%', '-50%']
            }},
            detail: {{
                valueAnimation: true,
                offsetCenter: ['0%', '-30%']
            }}
        }},
        {{
            value: {valor2Js},
            name: 'Good',
            title: {{
                offsetCenter: ['0%', '-10%']
            }},
            detail: {{
                valueAnimation: true,
                offsetCenter: ['0%', '10%']
            }}
        }},
        {{
            value: {valor3Js},
            name: 'Commonly',
            title: {{
                offsetCenter: ['0%', '30%']
            }},
            detail: {{
                valueAnimation: true,
                offsetCenter: ['0%', '50%']
            }}
        }}
    ];

    const gaugeData2 = [        
        {{
            value: {valor2Js},
            name: 'Good',
            title: {{
                offsetCenter: ['0%', '-10%']
            }},
            detail: {{
                valueAnimation: true,
                offsetCenter: ['0%', '10%']
            }}
        }}
    ];

    var option = {{
        series: [
            {{
                type: 'gauge',
                startAngle: 90,
                endAngle: -270,
                pointer: {{
                    show: false
                }},
                progress: {{
                    show: true,
                    overlap: false,
                    roundCap: true,
                    clip: false,
                    itemStyle: {{
                        borderWidth: 1,
                        borderColor: '#464646'
                    }}
                }},
                axisLine: {{
                    lineStyle: {{
                        width: 50
                    }}
                }},
                splitLine: {{
                    show: false,
                    distance: 0,
                    length: 10
                }},
                axisTick: {{
                    show: false
                }},
                axisLabel: {{
                    show: false,
                    distance: 50
                }},
                data: gaugeData,
                title: {{
                    fontSize: 14
                }},
                detail: {{
                    width: 50,
                    height: 15,
                    fontSize: 14,
                    color: 'inherit',
                    borderColor: 'inherit',
                    borderRadius: 20,
                    borderWidth: 1,
                    formatter: '{{value}}%'
                }}
            }}
        ]
    }};

    var option2 = {{
        series: [
            {{
                type: 'gauge',
                startAngle: 180,
                endAngle: 0,
                pointer: {{
                    show: false
                }},
                progress: {{
                    show: true,
                    overlap: false,
                    roundCap: true,
                    clip: false,
                    itemStyle: {{
                        borderWidth: 1,
                        borderColor: '#464646'
                    }}
                }},
                axisLine: {{
                    lineStyle: {{
                        width: 15
                    }}
                }},
                splitLine: {{
                    show: false,
                    distance: 0,
                    length: 10
                }},
                axisTick: {{
                    show: true
                }},
                axisLabel: {{
                    show: true,
                    distance: 25
                }},
                data: gaugeData2,
                title: {{
                    fontSize: 14
                }},
                detail: {{
                    width: 50,
                    height: 15,
                    fontSize: 14,
                    color: 'inherit',
                    borderColor: 'inherit',
                    borderRadius: 20,
                    borderWidth: 1,
                    formatter: '{{value}}%'
                }}
            }}
        ]
    }};

    myChart1.setOption(option);
    myChart2.setOption(option2);
    myChart3.setOption(option2);
    myChart4.setOption(option2);
    window.addEventListener('resize', myChart1.resize);
}});
";

        System.IO.File.WriteAllText(filePath, js);
    }


}