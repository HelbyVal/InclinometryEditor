import { Line, Scatter } from "react-chartjs-2";
import { WellDataModel } from "../Models/WellDataModel";
import { CategoryScale, ChartData, ChartDataset, DatasetChartOptions, Scale, scales } from "chart.js";
import Chart from 'chart.js/auto';

interface Props {
    wellData: WellDataModel[],
    mode : ChartMode,
    scale : number
}

export enum ChartMode {
    Vertical,
    Horizontal
}

export const MyChart = ({wellData, mode, scale} : Props) => {

    Chart.register(CategoryScale);
   
    const datasetVertical = { 
            label: "Вертикальное представление",
            data: wellData.map((d) => d.tvd)
    }

    const datasetHorizontal = {
        label: "Горизонтальное представление",
        data: wellData.map((d) => d.dN)
    }

    const optionsVert = {
        scales: {
            x: {
                min: -10 * scale,
                max: 10 * scale
            },       
            y: {
                min: -3000,
                max: 0
            }
        },
        showLine: true
    }

    const optionsHor = {
        scales: {
            x: {
                min: -30 * scale,
                max: 30 * scale
            },       
            y: {
                min: -30 * scale,
                max: 30 * scale
            }
        },
        showLine: true
    }

    let dataset = {
        label: "Горизонтальное представление",
        data: wellData.map((d) => d.dN)};
    let options = {
        scales: {
            x: {
                min: -3000,
                max: 3000
            },       
            y: {
                min: -3000,
                max: 3000
            }
        },
        showLine: true
    };

    if (mode == ChartMode.Horizontal) {
        dataset = datasetHorizontal;
        options = optionsHor;
    }
    else {
        dataset = datasetVertical;
        options = optionsVert;
    }

    const actions = [
        {
            name: "Вертикальное представление",
            handler(chart : any) {
                chart.data.datasets = [datasetVertical]
                chart.options.scales.y = { min: -3000, max: 0}
                chart.options.scales.x = { min: -100, max: 100}
            }
        },
        {
            name: "Горизонтальное представление",
            handler(chart : any) {
                chart.data.datasets = [datasetHorizontal]
                chart.options.scales.y = { min: -3000, max: 3000}
                chart.options.scales.x = { min: -3000, max: 3000}
            }
        }

    ]

    return(
        <Scatter 
            data = {{
                labels: wellData.map((d) => d.dE),
                datasets: [
                    dataset
                ]
            }}

            options = {options}
            
        />
    )
}