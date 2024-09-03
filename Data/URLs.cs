using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class URLs
    {
        public const string LOGIN = "/login";
        public const string REGISTER = "/register";
        public const string IDENTITY_CREATE_ROLE = "/identity_create_role";
        public const string IDENTITY_ADD_USER_TO_ROLE = "/identity_add_user_to_role";
        public const string IDENTITY_CHECK_USER_ADMIN_ROLE = "/identity_check_user_admin_role/{email}";

        public const string COMPUTERS = "/computers";
        public const string COMPUTERS_ID = "/computers/{id}";
        public const string COMPUTERS_CEVM_ID = "/computers/cevm/{id}";

        public const string MONITORS = "/monitors";
        public const string MONITORS_ID = "/monitors/{id}";
        public const string MONITORS_CEVM_ID = "/monitors/cevm/{id}";

        public const string STATUS = "/status";
        public const string STATUS_ID = "/status/{id}";
        public const string STATUS_CEVM_ID = "/status/cevm/{id}";

        public const string SOFTWARE = "/software";
        public const string SOFTWARE_ID = "/software/{id}";
        public const string SOFTWARE_CEVM_ID = "/software/cevm/{id}";

        public const string DEVICE = "/device";
        public const string DEVICE_ID = "/device/{id}";
        public const string DEVICE_CEVM_ID = "/device/cevm/{id}";

        public const string DEVICEMODEL = "/devicemodel";
        public const string DEVICEMODEL_ID = "/devicemodel/{id}";
        public const string DEVICEMODEL_CEVM_ID = "/devicemodel/cevm/{id}";

        public const string DEVICETYPE = "/devicetype";
        public const string DEVICETYPE_ID = "/devicetype/{id}";
        public const string DEVICETYPE_CEVM_ID = "/devicetype/cevm/{id}";

        public const string PRINTER = "/printer";
        public const string PRINTER_ID = "/printer/{id}";
        public const string PRINTER_CEVM_ID = "/printer/cevm/{id}";

        public const string PRINTERMODEL = "/printermodel";
        public const string PRINTERMODEL_ID = "/printermodel/{id}";
        public const string PRINTERMODEL_CEVM_ID = "/printermodel/cevm/{id}";

        public const string PRINTERTYPE = "/printertype";
        public const string PRINTERTYPE_ID = "/printertype/{id}";
        public const string PRINTERTYPE_CEVM_ID = "/printertype/cevm/{id}";

        public const string PHONE = "/phone";
        public const string PHONE_ID = "/phone/{id}";
        public const string PHONE_CEVM_ID = "/phone/cevm/{id}";

        public const string PHONEMODEL = "/phonemodel";
        public const string PHONEMODEL_ID = "/phonemodel/{id}";
        public const string PHONEMODEL_CEVM_ID = "/phonemodel/cevm/{id}";

        public const string PHONETYPE = "/phonetype";
        public const string PHONETYPE_ID = "/phonetype/{id}";
        public const string PHONETYPE_CEVM_ID = "/phonetype/cevm/{id}";

        public const string SIMCARD = "/simcard";
        public const string SIMCARD_ID = "/simcard/{id}";
        public const string SIMCARD_CEVM_ID = "/simcard/cevm/{id}";

        public const string SIMCOMPONENT = "/simcomponent";
        public const string SIMCOMPONENT_ID = "/simcomponent/{id}";
        public const string SIMCOMPONENT_CEVM_ID = "/simcomponent/cevm/{id}";

        public const string SIMCOMPONENTTYPE = "/simcomponenttype";
        public const string SIMCOMPONENTTYPE_ID = "/simcomponenttype/{id}";
        public const string SIMCOMPONENTTYPE_CEVM_ID = "/simcomponenttype/cevm/{id}";

        public const string NETWORKDEVICE = "/networkdevice";
        public const string NETWORKDEVICE_ID = "/networkdevice/{id}";
        public const string NETWORKDEVICE_CEVM_ID = "/networkdevice/cevm/{id}";

        public const string RACKCABINET = "/rackcabinet";
        public const string RACKCABINET_ID = "/rackcabinet/{id}";
        public const string RACKCABINET_CEVM_ID = "/rackcabinet/cevm/{id}";

        public const string RACKCABINETTYPE = "/rackcabinettype";
        public const string RACKCABINETTYPE_ID = "/rackcabinettype/{id}";
        public const string RACKCABINETTYPE_CEVM_ID = "/rackcabinettype/cevm/{id}";

        public const string RACKCABINETMODEL = "/rackcabinetmodel";
        public const string RACKCABINETMODEL_ID = "/rackcabinetmodel/{id}";
        public const string RACKCABINETMODEL_CEVM_ID = "/rackcabinetmodel/cevm/{id}";

        public const string MANUFACTURER = "/manufacturer";
        public const string MANUFACTURER_ID = "/manufacturer/{id}";
        public const string MANUFACTURER_CEVM_ID = "/manufacturer/cevm/{id}";

        public const string OS = "/operatingsystem";
        public const string OS_ID = "/operatingsystem/{id}";
        public const string OS_CEVM_ID = "/operatingsystem/cevm/{id}";

        public const string COMPUTERTYPE = "/computertype";
        public const string COMPUTERTYPE_ID = "/computertype/{id}";
        public const string COMPUTERTYPE_CEVM_ID = "/computertype/cevm/{id}";

        public const string COMPUTERMODEL = "/computermodel";
        public const string COMPUTERMODEL_ID = "/computermodel/{id}";
        public const string COMPUTERMODEL_CEVM_ID = "/computermodel/cevm/{id}";

        public const string MONITORTYPE = "/monitortype";
        public const string MONITORTYPE_ID = "/monitortype/{id}";
        public const string MONITORTYPE_CEVM_ID = "/monitortype/cevm/{id}";

        public const string MONITORMODEL = "/monitormodel";
        public const string MONITORMODEL_ID = "/monitormodel/{id}";
        public const string MONITORMODEL_CEVM_ID = "/monitormodel/cevm/{id}";

        public const string NETWORKDEVICETYPE = "/networkdevicetype";
        public const string NETWORKDEVICETYPE_ID = "/networkdevicetype/{id}";
        public const string NETWORKDEVICETYPE_CEVM_ID = "/networkdevicetype/cevm/{id}";

        public const string NETWORKDEVICEMODEL = "/networkdevicemodel";
        public const string NETWORKDEVICEMODEL_ID = "/networkdevicemodel/{id}";
        public const string NETWORKDEVICEMODEL_CEVM_ID = "/networkdevicemodel/cevm/{id}";

        public const string LOCATION = "/location";
        public const string LOCATION_ID = "/location/{id}";
        public const string LOCATION_CEVM_ID = "/location/cevm/{id}";

        public const string USER = "/user";
        public const string USER_ID = "/user/{id}";
        public const string USER_CEVM_ID = "/user/cevm/{id}";

        public const string SERVER = "/server";
        public const string SERVER_ID = "/server/{id}";
        public const string SERVER_CEVM_ID = "/server/cevm/{id}";

        public const string GROUPTYPE = "/grouptype";
        public const string GROUPTYPE_ID = "/grouptype/{id}";
        public const string GROUPTYPE_CEVM_ID = "/grouptype/cevm/{id}";

        public const string HARDDRIVEMODEL = "/harddrivemodel";
        public const string HARDDRIVEMODEL_ID = "/harddrivemodel/{id}";
        public const string HARDDRIVEMODEL_CEVM_ID = "/harddrivemodel/cevm/{id}";

        public const string HARDDRIVE = "/harddrive";
        public const string HARDDRIVE_ID = "/harddrive/{id}";
        public const string HARDDRIVE_CEVM_ID = "/harddrive/cevm/{id}";

        public const string MAINWINDOW = "/mainwindowhelper";
        public const string POSITION = "/position";
        public const string POSITION_ID = "/position/{id}";
        public const string POSITION_CEVM_ID = "/position/cevm/{id}";

        public const string CONTRACTTYPE = "/contracttype";
        public const string CONTRACTTYPE_ID = "/contracttype/{id}";
        public const string CONTRACTTYPE_CEVM_ID = "/contracttype/cevm/{id}";

        public const string KNOWLEDGEBASECATEGORY = "/knowledgebasecategory";
        public const string KNOWLEDGEBASECATEGORY_ID = "/knowledgebasecategory/{id}";
        public const string KNOWLEDGEBASECATEGORY_CEVM_ID = "/knowledgebasecategory/cevm/{id}";

        public const string KNOWLEDGEBASE = "/knowledgebase";
        public const string KNOWLEDGEBASE_ID = "/knowledgebase/{id}";
        public const string KNOWLEDGEBASE_CEVM_ID = "/knowledgebase/cevm/{id}";

        public const string LICENSETYPE = "/licensetype";
        public const string LICENSETYPE_ID = "/licensetype/{id}";
        public const string LICENSETYPE_CEVM_ID = "/licensetype/cevm/{id}";

        public const string LICENSE = "/license";
        public const string LICENSE_ID = "/license/{id}";
        public const string LICENSE_CEVM_ID = "/license/cevm/{id}";

        public const string OPERATINGSYSTEM = "/operatingsystem";
        public const string OPERATINGSYSTEM_ID = "/operatingsystem/{id}";
        public const string OPERATINGSYSTEM_CEVM_ID = "/operatingsystem/cevm/{id}";

        public const string TICKETCATEGORY = "/ticketcategory";
        public const string TICKETCATEGORY_ID = "/ticketcategory/{id}";
        public const string TICKETCATEGORY_CEVM_ID = "/ticketcategory/cevm/{id}";

        public const string TICKETSTATUS = "/ticketstatus";
        public const string TICKETSTATUS_ID = "/ticketstatus/{id}";
        public const string TICKETSTATUS_CEVM_ID = "/ticketstatus/cevm/{id}";

        public const string TICKETTYPE = "/tickettype";
        public const string TICKETTYPE_ID = "/tickettype/{id}";
        public const string TICKETTYPE_CEVM_ID = "/tickettype/cevm/{id}";

        public const string TICKET = "/ticket";
        public const string TICKET_ID = "/ticket/{id}";
        public const string TICKET_OWNER_ID = "/ticket/owner/{id}";
        public const string TICKET_CEVM_ID = "/ticket/cevm/{id}";

        public const string TECHNICIAN = "/technician";
        public const string TECHNICIAN_ID = "/technician/{id}";
        public const string TECHNICIAN_CEVM_ID = "/technician/cevm/{id}";


    }
}
