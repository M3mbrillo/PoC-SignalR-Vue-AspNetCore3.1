<template>
  <div class="home">
    <DxChart
      id="chart"
      :data-source="dataSource"
      palette="Violet"
    >
      <DxSeriesTemplate      
        name-field="serie"
      />
      <DxCommonSeriesSettings
        type="line"
        argument-field="xValue"
        value-field="yValue"
      />
    </DxChart>
    <br>
    <br>
    <label>serieName</label>
    <input 
      type="text"
      v-model="serieName"
    />
    <br>
    <br>
    <label>serie x value</label>
    <input
      type="number"
      v-model="xValue"
    />
    <br>
    <br>
    <label>serie y value</label>
      <input 
        type="number"
        v-model="yValue"
      />
    <br>
    <br>
    <button
      v-on:click="AddPoint()"
    >
      Add to all clients!!
    </button>
  </div>
</template>

<script>
import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

import DxChart, {
    // DxSeries,
    DxCommonSeriesSettings, DxSeriesTemplate
} from 'devextreme-vue/chart';
import * as signalR from '@aspnet/signalr';

export default {
  name: "Home",
  data() {
    return {
      hubConnection: undefined,
      serieName: '',
      xValue: 0,
      yValue: 0,
      dataSource: [{
        serie: 'serie1',
        xValue: 10,
        yValue: 10
      }],      
    }
  },
  components: {
    // DxSeries,
    DxCommonSeriesSettings, DxChart, DxSeriesTemplate
  },
  methods: {
    AddPoint: function () {
      const data = {
        serie: this.serieName,
        xValue: +(this.xValue),
        yValue: +(this.yValue)
      }
      axios.post('https://localhost:5001/Api/MockData/AddPoint', data)
    }
  },
  created: function () {  
    this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(
        'https://localhost:5001/Hubs/MockData',
        {
          transport: signalR.HttpTransportType.WebSockets,
          logMessageContent: true,
          logger: signalR.LogLevel.Trace,
        }
      )
      // .withAutomaticReconnect()
      .withHubProtocol(new signalR.JsonHubProtocol()) 
      .build();

      this.hubConnection.on('AddedPoint', (newPoint) => {this.dataSource.push(newPoint)});
      
      this.hubConnection
        .start()
        .then(() => console.info('SignalR Connected'))
        .catch(err => console.error('SignalR Connection Error: ', err));
  }
};
</script>
