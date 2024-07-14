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

import {useKeycloak} from "@react-keycloak/web";
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

  const { keycloak, initialized } = useKeycloak();

  useEffect(() => {
    if (!initialized) {
      console.log(keycloak.token);
      
      return;
    }

    if (!keycloak.authenticated) {    
      keycloak.login();
      
    } 

    const getWells = async () => {
      const wells = await GetWells(keycloak.token);
      setLoading(false);
      wells.forEach((el: any) => {
        el.createDate = dayjs(el.createDate).format("YYYY-MM-DD");
      });
      setWells(wells);
    }

    getWells();

    setWellData([defaultData]);
  }, [keycloak, initialized])

  const handleCreateWell = async (request: WellRequest) => {
    await AddWell(request, keycloak.token);
    closeWellForm();

    setWells(await GetWells(keycloak.token))
    setActiveWell(defaultWell);
  }

  const handleUpdateWell = async (Id: string, request: WellRequest ) => {
    await UpdateWell(Id, request, keycloak.token);
    closeWellForm();

    setWells(await GetWells(keycloak.token));
  }

  const handleDeleteWell = async (Id: string) => {
    await DeleteWell(Id, keycloak.token);

    setWells(await GetWells(keycloak.token));
    if (activeWell.id == Id) {
      setActiveWell(defaultWell);
      setWellData([]);
    }
  }

  const handleActivateWell = async (well: WellModel) => {
    const response = await GetWellData(well.id, keycloak.token);
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
    await AddWellData(well.id, request, keycloak.token);

    setWellData(await GetWellData(well.id, keycloak.token));
    closeDataForm();
  }

  const handleDeleteWellData = async () => {
    await DeleteWellData(activeWell.id, keycloak.token);

    setWellData(await GetWellData(activeWell.id, keycloak.token));
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
