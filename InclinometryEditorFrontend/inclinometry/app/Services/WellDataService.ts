export interface WellDataRequest {
    inclination: number | undefined,
    azimut: number | undefined,
    md: number | undefined
}

export const GetWellData = async (wellId: string) => {
    const response = await fetch(`http://localhost:5021/GetWellData?wellId=${wellId}`);
    return response.json();
};

export const AddWellData = async (wellId: string, request: WellDataRequest) => {
    const response = await fetch(`http://localhost:5021/AddWellData?wellId=${wellId}&inclination=${request.inclination}&azimut=${request.azimut}&md=${request.md}`,
        {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
        }
    );
}

export const DeleteWellData = async (wellId: string) => {
    const response = await fetch(`http://localhost:5021/DeleteWellData?wellId=${wellId}`,
        {
            method: "DELETE",
            headers: {
                "content-type": "application/json"
            },
        }
    );
}