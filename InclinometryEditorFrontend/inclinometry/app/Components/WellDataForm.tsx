import { useState } from "react";
import { WellDataRequest } from "../Services/WellDataService";
import { WellModel } from "../Models/WellModel";
import { InputNumber, Modal } from "antd";

interface Props {
    well: WellModel
    isModalOpen: boolean,
    handleCancel: () => void,
    handleCreate: (well: WellModel, request : WellDataRequest) => void
}

export const WellDataForm = ({well, isModalOpen, handleCancel, handleCreate}: Props) => {
    const [azimut, setAzimut] = useState<number>();
    const [inclination, setInclination] = useState<number>();
    const [md, setMd] = useState<number>();

    const handleOk = async () => {
        const request = { azimut, inclination, md}
        handleCreate(well, request)
    }

    return(
        <Modal 
            title = {"Добавить запись в иклинометрию скважины " + well.title} 
            open = {isModalOpen}
            cancelText={"Отмена"}
            onOk= {handleOk}
            onCancel = {handleCancel}>
    
            <div>
                <InputNumber
                    value = {azimut}
                    onChange={(e : number) => setAzimut(e)}
                    stringMode = {true}
                    placeholder = "Азимут"
                />
                <InputNumber
                    value = {inclination}
                    onChange={(e : number) => setInclination(e)}
                    stringMode = {true}
                    placeholder = "Искривление"
                />
                <InputNumber
                    value = {md}
                    onChange={(e : number) => setMd(e)}
                    stringMode = {true}
                    placeholder = "Глубина по стволу"
                />
            </div>
        </Modal>
        )
}