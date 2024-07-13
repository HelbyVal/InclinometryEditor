import dayjs from 'dayjs'
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
import { Dayjs } from "dayjs";

export interface WellRequest {
    title: string,
    description: string,
    createDate: Dayjs 
}

export const GetWells = async () => {
    const response = await fetch("http://localhost:5021/GetWells");
    return response.json();
};

export const AddWell = async (wellRequest : WellRequest) => {
    let s = wellRequest.createDate.format("YYYY-MM-DD").toString();

    const response = await fetch(`http://localhost:5021/AddWell?title=${wellRequest.title}&description=${wellRequest.description}&createDate=${s}`,
        {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
    });
    return response.json();
};

export const UpdateWell = async (id: string, wellRequest : WellRequest) => {
    const response = await fetch(`http://localhost:5021/UpdateWell?wellId=${id}&title=${wellRequest.title}&description=${wellRequest.description}&createDate=${wellRequest.createDate.format("YYYY-MM-DD").toString()}`, {
        method: "PATCH",
        headers: {
            "content-type": "application/json",
            "server": "Kastrel",
            "transfer-encoding": "chuncked"
        },
    });
    return response.json();
};

export const DeleteWell = async (id: string) => {
    const response = await fetch(`http://localhost:5021/DeleteWell?wellId=${id}`, {
        method: "DELETE",
        headers: {
            "content-type": "application/json; charset=utf-8",
            "server": "Kastrel",
            "transfer-encoding": "chuncked"
        },
    });
};