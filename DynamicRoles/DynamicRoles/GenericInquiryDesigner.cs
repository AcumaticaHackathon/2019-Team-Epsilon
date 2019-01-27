using System;
using PX.Objects;
using PX.Data;
using PX.Data.Maintenance.GI;

namespace DynamicRoles
{
  public class GenericInquiryDesigner_Extension : PXGraphExtension<GenericInquiryDesigner>
  {

    #region Event Handlers
        public void GIDesign_UsrESIsDynamicRole_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
        {
            
            bool? Checked = (bool?)e.NewValue;
            if (Checked != true)
                return;
            GenericInquiryDesigner graph = (GenericInquiryDesigner)sender.Graph;
            bool HasUsers = false;
            bool HasEmployees = false;
            foreach(GITable Table in graph.Tables.Select())
            {
                if (Table.Name == "PX.SM.Users")
                    HasUsers = true;
                if (Table.Name == "PX.Objects.EP.EPEmployee")
                    HasEmployees = true;
            }
            if(HasUsers == false || HasEmployees == false)
                throw new PXSetPropertyException("Tables PX.SM.Users and PX.Objects.EP.EPEmployee must be added to enable this feature.", PXErrorLevel.Error);
        }

        public void GITable_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
        {

            GenericInquiryDesigner graph = (GenericInquiryDesigner)sender.Graph;
            GIDesign CurrentGI = graph.Designs.Current;
            if (CurrentGI == null)
                return;
            GIDesignExt CurrentGIExt = CurrentGI.GetExtension<GIDesignExt>();
            if (CurrentGIExt.UsrESIsDynamicRole != true)
                return;
            else
            {
                object NewValue = CurrentGIExt.UsrESIsDynamicRole;
                sender.RaiseFieldVerifying<GIDesignExt.usrESIsDynamicRole>(CurrentGI, ref NewValue);
            }
        }
    #endregion
    }
}