import { DatePicker, Input, Modal } from "antd";
import { WellRequest } from "../Services/WellService";
import { useEffect, useState } from "react";
import TextArea from "antd/es/input/TextArea";
import dayjs, { Dayjs } from "dayjs";
import { WellModel } from "../Models/WellModel";
import advancedFormat from 'dayjs/plugin/advancedFormat'
import customParseFormat from 'dayjs/plugin/customParseFormat'
import localeData from 'dayjs/plugin/localeData'
import weekday from 'dayjs/plugin/weekday'
import weekOfYear from 'dayjs/plugin/weekOfYear'
import weekYear from 'dayjs/plugin/weekYear'

dayjs.extend(customParseFormat)
dayjs.extend(advancedFormat)
dayjs.extend(weekday)
dayjs.extend(localeData)
dayjs.extend(weekOfYear)
dayjs.extend(weekYear)

interface Props {
    mode: Mode;
    value: WellModel;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: WellRequest) => void;
    handleUpdate: (id: string, Request: WellModel) => void;  
}

export enum Mode {
    Create,
    Edit
}

export const OpenWellForm = ({
    mode,
    value,
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate} :Props) => {
        
        const [title, setTitle] = useState<string>("");
        const [description, setDescription] = useState<string>("");
        const [createDate, setDateCreate] = useState<Dayjs>(dayjs(new Date().toDateString()));

        useEffect(() => {
            setTitle(value.title);
            setDescription(value.description);
            setDateCreate(dayjs(value.createDate));
        }, [value]);

        const handleOk = async () => {
            const id = value.id;
            const WellRequest = { id, title, description, createDate};
            mode == Mode.Create ? handleCreate(WellRequest) : handleUpdate(value.id, WellRequest)
        }

        return(
        <Modal 
            title = {mode === Mode.Create ? "Добавление скважины" : "Редактирование скважины"} 
            open = {isModalOpen}
            cancelText={"Отмена"}
            onOk= {handleOk}
            onCancel = {handleCancel}>
    
            <div>
                <Input
                    value = {title}
                    onChange={(e : any) => setTitle(e.target.value)}
                    placeholder = "Название"
                />
                <TextArea 
                    value = {description}
                    onChange = {(e : any) =>  setDescription(e.target.value)}
                    autoSize = {{minRows: 3, maxRows: 3}}
                    placeholder = "Описание"    
                />
                <DatePicker
                    value = {createDate}
                    onChange={(e) => setDateCreate(e)}
                    placeholder="Дата создания"
                />
            </div>
        </Modal>
        )
};