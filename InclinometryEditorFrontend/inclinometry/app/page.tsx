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

import Chart from 'chart.js/auto';
import { useEffect, useState } from "react";
import { AddWell, DeleteWell, GetWells, UpdateWell, WellRequest } from "./Services/WellService";
import { Wells } from "./Components/Well";
import Title from "antd/es/skeleton/Title";
import { OpenWellForm, Mode } from "./Components/WellForm";
import Button from "antd/es/button/button"
import { WellModel } from "./Models/WellModel";
import { WellDataModel, defaultData } from './Models/WellDataModel';
import { AddWellData, GetWellData, WellDataRequest, DeleteWellData } from './Services/WellDataService';
import { WellDataTable } from './Components/WellDataTable';
import { WellDataForm } from './Components/WellDataForm';
import { CloseOutlined, PlusOutlined, ZoomInOutlined, ZoomOutOutlined } from '@ant-design/icons';
import { Line, Scatter } from 'react-chartjs-2';
import { ChartMode, MyChart } from './Components/Chart';
import Segmented from 'antd/es/segmented';
import { Slider, SliderSingleProps } from 'antd';


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

  const [wellData, setWellData] = useState<WellDataModel[]>([]);
  const [activeWell, setActiveWell] = useState<WellModel>(defaultWell);
  const [isModalOpenData, setModalOpenData] = useState<boolean>(false);

  const [chartMode, setChartMode] = useState<ChartMode>(ChartMode.Vertical);
  const [chartScale, setChartScale] = useState<number>(100);

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

    setWellData([defaultData]);
  }, [])

  const handleCreateWell = async (request: WellRequest) => {
    await AddWell(request);
    closeWellForm();

    setWells(await GetWells())
    setActiveWell(defaultWell);
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
    if (activeWell.id == Id) {
      setActiveWell(defaultWell);
      setWellData([]);
    }
  }

  const handleActivateWell = async (well: WellModel) => {
    const response = await GetWellData(well.id);
    setWellData(response);
    setActiveWell(well);
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

  const handleCreateWellData = async (well: WellModel, request : WellDataRequest) => {
    await AddWellData(well.id, request);

    setWellData(await GetWellData(well.id));
    closeDataForm();
  }

  const handleDeleteWellData = async () => {
    await DeleteWellData(activeWell.id);

    setWellData(await GetWellData(activeWell.id));
  }

  const OpenWellDataForm = () => {
    setModalOpenData(true);
  }

  const closeDataForm = () => {
    setModalOpenData(false);
  }

  const formatter: NonNullable<SliderSingleProps['tooltip']>['formatter'] = (value) => `${value}%`;

  return (
    <div>
      <Button
        icon = {<PlusOutlined />}
        onClick = {openWellForm}
      />

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
        handleActivate={handleActivateWell}
        />
        )}

      <Button
        icon = {<PlusOutlined />}
        onClick = {OpenWellDataForm}
        disabled = {activeWell.id == defaultWell.id}
      />
      <Button
        icon = {<CloseOutlined />}
        onClick = {handleDeleteWellData}
        disabled = {activeWell.id == defaultWell.id || wellData.length == 1}
        danger
      />
      <WellDataTable
        wellData={wellData}
        well = {activeWell}
      />

      <WellDataForm
        well={activeWell}
        isModalOpen= {isModalOpenData}
        handleCancel={closeDataForm}
        handleCreate={handleCreateWellData}
      />

      <Segmented
        options={['Вертикальное представление', 'Горизонтальное представление']}
        onChange={(value: string) => {
          if (value == 'Вертикальное представление') {
            setChartMode(ChartMode.Vertical);
          }
          else if (value == 'Горизонтальное представление') {
            setChartMode(ChartMode.Horizontal);
          }
        }}
      />

      <MyChart
        wellData={wellData}
        mode={chartMode}
        scale={chartScale}
      />

      <ZoomOutOutlined />
      <Slider
        tooltip = {{ formatter }}
        onChange = {(value: number) => {
          setChartScale(value)
        }}   
      />
      <ZoomInOutlined />
    </div>
  );
}
