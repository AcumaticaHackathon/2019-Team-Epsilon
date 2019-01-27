using System;
using System.Collections.Generic;
using PX.Data;
using PX.SM;

namespace DynamicRoles
{
    public class DynamicRoleProcess : PXGraph<DynamicRoleProcess>
    {


        public PXFilter<ProcessFilter> Filter;

        [Serializable]
        public class ProcessFilter : IBqlTable
        {

        }


        public PXFilteredProcessingJoinGroupBy<Roles, ProcessFilter,
            InnerJoin<ESDynamicRole, On<Roles.rolename, Equal<ESDynamicRole.rolename>>>,
            Where<ESDynamicRole.rolename, Equal<ESDynamicRole.rolename>>,
            Aggregate<GroupBy<Roles.rolename>>> DynamicRoles;



        public DynamicRoleProcess()
        {
            DynamicRoles.SetProcessCaption("Update");
            DynamicRoles.SetProcessAllCaption("Update All");
            DynamicRoles.SetProcessDelegate<RoleAccess>(
            delegate (RoleAccess graph, Roles role)
            {
                graph.Clear();
                graph.Roles.Current = role;
                RoleAccess_Extension graphExt = graph.GetExtension<RoleAccess_Extension>();
                graphExt.eSResetDynamicAccess.Press();
                graph.Save.Press();
                System.Threading.Thread.Sleep(100);
            });
        }
    }

}