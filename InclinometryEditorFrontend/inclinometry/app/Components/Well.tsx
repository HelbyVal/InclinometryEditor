import Button from "antd/es/button/button"
import { WellModel } from "../Models/WellModel";
import Card from "antd/es/card/Card";

interface Props {
    wells: WellModel[];
    handleDelete: (id: string) => void;
    handleOpen: (id: string, well: WellModel) => void;
}

export const Wells = ({wells, handleOpen, handleDelete}: Props) => {
    return(
        <div className="Card">
            {
                wells.map((well : WellModel) => (
                    <Card key = {well.id} title= {well.title}>
                        <p>{well.description}</p>
                        <div className="Buttons">
                            <Button 
                                onClick={() => handleOpen(well.id, well)}
                                style={{flex: 1}}>
                                    Редактировать</Button>
                            <Button
                                onClick={() => handleDelete(well.id)}
                                danger
                                style={{flex: 1}}
                            >
                                Удалить
                            </Button>
                        </div>
                    </Card>
                ))
            }
        </div>
    )
}