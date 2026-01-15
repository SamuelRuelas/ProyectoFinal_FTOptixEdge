#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.ODBCStore;
using FTOptix.Store;
using FTOptix.DataLogger;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    private IUAVariable msgVar;
    public override void Start()
    {
        msgVar = Project.Current.Get<IUAVariable>("Model/Texto");
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void setMessage()
    {
        string texto = "Nuevo mensaje establecido a las " + DateTime.Now.ToString("HH:mm:ss");
        msgVar.Value = texto;
    }
}
