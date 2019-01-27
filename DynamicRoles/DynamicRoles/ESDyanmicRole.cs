using System;
using PX.Data;
using PX.Data.Maintenance.GI;
using PX.SM;

namespace DynamicRoles
{
    [Serializable]
    public class ESDynamicRole : IBqlTable
    {
        #region DynamicRoleID
        [PXInt(IsKey = true)]
        [PXUIField(DisplayName = "Dynamic Role ID")]
        public virtual int? DynamicRoleID { get; set; }
        public abstract class dynamicRoleID : IBqlField { }
        #endregion


        #region Rolename
        [PXDBString(64, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Rolename")]
        [PXDBDefault(typeof(Roles.rolename))]
        [PXParent(typeof(Select<Roles,
            Where<Roles.rolename, Equal<Current<Roles.rolename>>>>))]
        public virtual string Rolename { get; set; }
        public abstract class rolename : IBqlField { }
        #endregion


        #region DesignID
        [PXDBGuid ()]
        [PXUIField(DisplayName = "Generic Inquiry")]
        [PXSelector(typeof(Search<GIDesign.designID>), typeof(GIDesign.name), SubstituteKey = typeof(GIDesign.name))]
        public virtual Guid? DesignID { get; set; }
        public abstract class designID : IBqlField { }
        #endregion

        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        protected Guid? _CreatedByID;
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID
        {
            get
            {
                return this._CreatedByID;
            }
            set
            {
                this._CreatedByID = value;
            }
        }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }
        protected string _CreatedByScreenID;
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID
        {
            get
            {
                return this._CreatedByScreenID;
            }
            set
            {
                this._CreatedByScreenID = value;
            }
        }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _CreatedDateTime;
        [PXDBCreatedDateTime()]
        [PXUIField(DisplayName = "Created On")]
        public virtual DateTime? CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                this._CreatedDateTime = value;
            }
        }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }
        protected Guid? _LastModifiedByID;
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID
        {
            get
            {
                return this._LastModifiedByID;
            }
            set
            {
                this._LastModifiedByID = value;
            }
        }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }
        protected string _LastModifiedByScreenID;
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID
        {
            get
            {
                return this._LastModifiedByScreenID;
            }
            set
            {
                this._LastModifiedByScreenID = value;
            }
        }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _LastModifiedDateTime;
        [PXDBLastModifiedDateTime()]
        [PXUIField(DisplayName = "Last Modified On")]
        public virtual DateTime? LastModifiedDateTime
        {
            get
            {
                return this._LastModifiedDateTime;
            }
            set
            {
                this._LastModifiedDateTime = value;
            }
        }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }
        protected Guid? _NoteID;
        [PXNote()]
        public virtual Guid? NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        protected byte[] _tstamp;
        [PXDBTimestamp()]
        public virtual byte[] tstamp
        {
            get
            {
                return this._tstamp;
            }
            set
            {
                this._tstamp = value;
            }
        }
        #endregion
    }
}