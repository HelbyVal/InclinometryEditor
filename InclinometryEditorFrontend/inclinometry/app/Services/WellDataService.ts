export interface WellDataRequest {
    inclination: number | undefined,
    azimut: number | undefined,
    md: number | undefined
}

export const GetWellData = async (wellId: string, token : string | undefined) => {
    const response = await fetch(`http://localhost:5021/GetWellData?wellId=${wellId}`, {
        method: "GET",
        headers: {
            "content-type": "application/json",
            "authorization": `Bearer ${token}`,
            "Access-Control-Allow-Origin": "*"
        }, 
    });
    return response.json();
};

export const AddWellData = async (wellId: string, request: WellDataRequest, token : string | undefined) => {
    const response = await fetch(`http://localhost:5021/AddWellData?wellId=${wellId}&inclination=${request.inclination}&azimut=${request.azimut}&md=${request.md}`,
        {
            method: "POST",
            headers: {
                "content-type": "application/json",
                "authorization": `Bearer ${token}`,
                "Access-Control-Allow-Origin": "*"
            },
        }
    );
}

export const DeleteWellData = async (wellId: string, token: string | undefined) => {
    const response = await fetch(`http://localhost:5021/DeleteWellData?wellId=${wellId}`,
        {
            method: "DELETE",
            headers: {
                "content-type": "application/json",
                "authorization": `Bearer ${token}`,
                "Access-Control-Allow-Origin": "*"
            },
        }
    );
}