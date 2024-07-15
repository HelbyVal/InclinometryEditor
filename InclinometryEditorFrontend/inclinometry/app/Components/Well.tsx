
import "../page.module.css";
import Button from "antd/es/button/button"
import { WellModel } from "../Models/WellModel";
import Card from "antd/es/card/Card";
import { CloseOutlined, EditOutlined, EyeOutlined } from "@ant-design/icons";

interface Props {
    wells: WellModel[];
    handleDelete: (id: string) => void;
    handleOpen: (id: string, well: WellModel) => void;
    handleActivate: (well : WellModel) => void;
}

export const Wells = ({wells, handleOpen, handleDelete, handleActivate}: Props) => {
    return(
        <div className="Card">
            {
                wells.map((well : WellModel) => (
                    <Card className = "card"
                          style = {{width: 300}}
                          key = {well.id}
                          title= {well.title}
                          actions={
                            [
                                <Button
                                className = "ActivateButton"
                                onClick = {() => handleActivate(well)}
                                icon = {<EyeOutlined/>}/>,

                                <Button 
                                onClick={() => handleOpen(well.id, well)}
                                icon = {<EditOutlined/>}/>,

                                <Button
                                onClick={() => handleDelete(well.id)}
                                danger
                                icon = {<CloseOutlined/>}/>,
                            ]
                          }
                          >
                        <p>{well.description}</p>
                        <div className="Buttons">
                            
                            
                        </div>
                    </Card>
                ))
            }
        </div>
    )
}