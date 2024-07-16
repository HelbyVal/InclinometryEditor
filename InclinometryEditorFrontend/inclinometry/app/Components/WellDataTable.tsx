import { Table } from "antd";
import { WellDataModel } from "../Models/WellDataModel";
import { WellModel } from "../Models/WellModel";

interface Props {
    well: WellModel,
    wellData: WellDataModel[],
}

export const WellDataTable = ({wellData, well}: Props) => {
const colums = 
    [
        {
            title: 'Искривление',
            dataIndex: 'inclination',
            key: 'Inclination'
        },
        {
            title: 'Азимут',
            dataIndex: 'azimut',
            key: 'Azimut' 
        },
        {
            title: 'Глубина по стволу',
            dataIndex: 'md',
            key: 'Md'
        },
        {
            title: 'Вертикальная глубина',
            dataIndex: 'tvd',
            key: 'TVD'
        },
        {
            title: 'Отклонение на восток',
            dataIndex: 'dE',
            key: 'dE'
        },
        {
            title: 'Отклонение на север',
            dataIndex: 'dN',
            key: 'dN'
        },
        {
            title: 'Темп набора кривизны',
            dataIndex: 'dls',
            key: 'DLS'
        },
    ]
    let tlt = "";
    if (!(well.title == "" || well.title == undefined)) {
        tlt = "Скважина " + well.title;
    }

    return(
    <div className="Table">
        <h2>{tlt}</h2>
        <Table 
            scroll = {{y: 300}}
            columns = {colums}
            dataSource = {wellData}
            pagination = {false}
            />
    </div>
)
}