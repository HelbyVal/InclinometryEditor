"use client";
import dayjs, { Dayjs } from 'dayjs'
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


import { useEffect, useState } from "react";
import { AddWell, DeleteWell, GetWells, UpdateWell, WellRequest } from "./Services/WellService";
import { Wells } from "./Components/Well";
import Title from "antd/es/skeleton/Title";
import { OpenWellForm, Mode } from "./Components/WellForm";
import Button from "antd/es/button/button"
import { WellModel } from "./Models/WellModel";

export default function Home() {
  const defaultWell = {
    title: "",
    description: "",
    createDate: dayjs(new Date().toDateString())
  } as WellModel

  const [valueWell, setValueWell] = useState<WellModel>(defaultWell);

  const [wells, setWells] = useState<WellModel[]>([])
  const [loading, setLoading] = useState(true);
  const [isModalOpenWell, setIsModalOpenWell] = useState(false);
  const [mode, setMode] = useState(Mode.Create);

  useEffect(() => {
    const getWells = async () => {
      const wells = await GetWells();
      setLoading(false);
      wells.forEach((el: any) => {
        el.createDate = dayjs(el.createDate).format("YYYY-MM-DD");
      });
      setWells(wells);
    }

    getWells();
  }, [])

  const handleCreateWell = async (request: WellRequest) => {
    await AddWell(request);
    closeWellForm();

    setWells(await GetWells())
    console.log(wells);
  }

  const handleUpdateWell = async (Id: string, request: WellRequest ) => {
    await UpdateWell(Id, request);
    closeWellForm();

    setWells(await GetWells());
  }

  const handleDeleteWell = async (Id: string) => {
    console.log(Id);
    await DeleteWell(Id);

    setWells(await GetWells());
  }

  const openWellForm = () => {
    setMode(Mode.Create);
    setIsModalOpenWell(true);
  }

  const closeWellForm = () => {
    setIsModalOpenWell(false);
    setValueWell(defaultWell);
  }

  const openModalWellEdit = (Id: string, well: WellModel) => {

    let w = dayjs("2023-05-05").format("YYYY-MM-DD");
    let s = well;
    setMode(Mode.Edit);
    setValueWell(well);
    setIsModalOpenWell(true);

  }

  return (
    <div>
      <Button
        type = "primary"
        style = {{marginTop: "30px"}}
        size = "large"
        onClick = {openWellForm}
      >
        Добавить скважину
      </Button>

      <OpenWellForm
        mode = {mode}
        value={valueWell}
        isModalOpen = {isModalOpenWell}
        handleCancel={closeWellForm}
        handleCreate={handleCreateWell}
        handleUpdate={handleUpdateWell}
      />

      {loading ? ( <Title>Loading</Title> ) : ( <Wells
        wells = {wells}
        handleDelete={handleDeleteWell}
        handleOpen={openModalWellEdit}
        />
        )}
    </div>
  );
}
