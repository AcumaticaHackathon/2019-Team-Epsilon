using APQuickCheck = PX.Objects.AP.Standalone.APQuickCheck;
using CRLocation = PX.Objects.CR.Standalone.Location;
using IRegister = PX.Objects.CM.IRegister;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.MigrationMode;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using PX.Data.Maintenance.GI;

namespace DynamicRoles
{
  public class GIDesignExt : PXCacheExtension<GIDesign>
  {
        #region usrESIsDynamicRole
        [PXDBBool]
        [PXUIField(DisplayName = "Is Dynamic Role?")]
        

    public virtual bool? UsrESIsDynamicRole { get; set; }
    public abstract class usrESIsDynamicRole : IBqlField { }
    #endregion
  }
}