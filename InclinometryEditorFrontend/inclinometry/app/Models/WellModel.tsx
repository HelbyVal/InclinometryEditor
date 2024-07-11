import { Dayjs } from "dayjs";

export interface WellModel {
    id : string;
    title : string;
    description : string;
    createDate: Dayjs;
}