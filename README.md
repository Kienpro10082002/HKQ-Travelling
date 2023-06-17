# HKQ-Travelling
Khi add dữ liệu bằng linq thì sẽ bị lỗi.
Cách khắc phụ như sau:
Model -> HKQTravel.dbml -> HKQTravel.designer.cs -> thêm dòng sau :
public HKQTravelDataContext() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["HKQTravelConnectionString"].ConnectionString, mappingSource)
        {
            OnCreated();
        }
