export interface WellDataModel {
    id : string
    num : number,
    md : number,
    inclination : number,
    azimut : number,
    tvd : number,
    dE: number,
    dN: number,
    dls: number,
    z: number,
    y: number,
    x: number
}

export const DefaultData = {
    id : "",
    num : 0,
    md : 0,
    inclination : 0,
    azimut : 0,
    tvd : 0,
    dE: 0,
    dN: 0,
    dls: 0,
    z: 0,
    y: 0,
    x: 0
}  as WellDataModel

export const defaultData = {} as WellDataModel