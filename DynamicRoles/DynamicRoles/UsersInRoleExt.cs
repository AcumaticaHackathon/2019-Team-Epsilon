using PX.Data;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PX.SM
{
    public class UsersInRolesExt : PXCacheExtension<UsersInRoles>
    {
        #region usrESDirectAssigned
        public abstract class usrESDirectAssigned : PX.Data.IBqlField
        {
        }
        protected bool? _usrESDirectAssigned;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Is Direct Assigned?", Visible = true, Enabled = false)]
        public virtual bool? UsrESDirectAssigned
        {
            get
            {
                return this._usrESDirectAssigned;
            }
            set
            {
                this._usrESDirectAssigned = value;
            }
        }
        #endregion
    }

}