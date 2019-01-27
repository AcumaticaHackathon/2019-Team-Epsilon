using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace DynamicRoles
{
  public class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
  {
    #region UsrJobTitle
    [PXDBString]
    [PXUIField(DisplayName="Job Title")]
    public virtual string UsrJobTitle { get; set; }
    public abstract class usrJobTitle : IBqlField { }
    #endregion
  }
}