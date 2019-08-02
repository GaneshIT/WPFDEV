using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace IMMO.BIM.TOOL
{
    public static class EquipmentData
    {
        public static string UpdateEquipment(string table,string updatedcolumns,string equipId)
        {
            string msg = string.Empty;
            string query = "update " + table + " set " + updatedcolumns + " where id=" + equipId + " and gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            msg = DataConnection.ExecuteQuery(query);
            return msg;
        }
        public static string DeleteEquipment(string table, string equipId)
        {
            string msg = string.Empty;
            string query = "delete from  " + table +" where id=" + equipId + " and gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            msg = DataConnection.ExecuteQuery(query);
            return msg;
        }
        
    }
}
