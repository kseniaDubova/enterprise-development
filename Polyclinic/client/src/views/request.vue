<template>
  <div class="request">
    <RequestContent  
      :requests="requestsList" 
      :doctors="doctors"
      @update="updateList"
      @doctorId="setSearchId"
    />
  </div>
</template>

<script>
import RequestContent from "@/components/shared/request/request.vue"
import Api from "@/api/api";

const api = new Api();

export default {
    name: "Request",

    components: {
      RequestContent,
    },

    data() {
      return {
        requestsList: [
          { id: 0,
            text: "Вывести информацию о всех врачах, стаж работы которых не меньше 10 лет",
            request: null,
            search_id: false,
            grid: "1fr 2fr 2fr 2fr 2fr 2fr",
            headers: [{label: "id"}, {label:"Паспорт"}, {label:"Имя"}, {label:"Дата рождения"}, {label:"Стаж"}, {label:"Специализация"}],
          },
          { id: 1,
            text: "Вывести информацию о всех пациентах, записанных на прием к указанному врачу, упорядочить по ФИО",
            request: null,
            search_id: true,
            grid: "1fr 2fr 2fr 2fr 2fr",
            headers: [{label: "id"}, {label:"Паспорт"}, {label:"Имя"}, {label:"Дата рождения"}, {label:"Адрес"}],
          },
          { id: 2,
            text: "Вывести информацию о здоровых на настоящий момент пациентах",
            request: null,
            search_id: false,
            grid: "1fr 2fr 2fr 2fr 2fr",
            headers: [{label: "id"}, {label:"Паспорт"}, {label:"Имя"}, {label:"Дата рождения"}, {label:"Адрес"}],
          },
          { id: 3,
            text: "Вывести информацию о количестве приемов пациентов по врачам за последний месяц",
            request: null,
            search_id: false,
            grid: "1fr 1fr",
            headers: [{label: "Доктор"}, {label:"Количество"}],
          
          },
          { id: 4,
            text: "Вывести информацию о топ 5 наиболее распространенных заболеваниях cреди пациентов",
            request: null,
            search_id: false,
            grid: "1fr 1fr",
            headers: [{label: "Специализация"}, {label:"Количество"}],
          },
          { id: 5,
            text: "Вывести информацию о пациентах старше 30 лет, которые записаны на прием к нескольким врачам, упорядочить по дате рождения",
            request: null,
            search_id: false,
            grid: "1fr 2fr 2fr 2fr 2fr",
            headers: [{label: "id"}, {label:"Паспорт"}, {label:"Имя"}, {label:"Дата рождения"}, {label:"Адрес"}],
          },
        ],
        search_id: null,
        doctors: [],
      }
    },

    async created() {
        this.getDoctors();
    },

    methods: {
      async getExperienceDoctors() {
        this.requestsList[0].request = await api.getExperienceDoctors();
      },

      async getPatientsOfDoctors() {
        this.requestsList[1].request = this.search_id ? await api.getPatientsOfDoctors(this.search_id) : "";
      },

      async getHealthyPatients() {
        this.requestsList[2].request = await api.getHealthyPatients();
      },

      async getCountAppointment() {
        this.requestsList[3].request = await api.getCountAppointment();
      },

      async getDiseaseTop() {
        this.requestsList[4].request = await api.getDiseaseTop();
      },

      async getPatientWithSeveralAppointment() {
        this.requestsList[5].request = await api.getPatientWithSeveralAppointment();
      },

      async updateList(id) {
        const actions = {
          0: this.getExperienceDoctors,
          1: this.getPatientsOfDoctors,
          2: this.getHealthyPatients,
          3: this.getCountAppointment,
          4: this.getDiseaseTop,
          5: this.getPatientWithSeveralAppointment,
        };

        if (actions[id]) {
          await actions[id](); 
        }
      },

      async getDoctors() {
        this.doctors = await api.getDoctors();
      },

      setSearchId(id) {
        this.search_id = id;
      }
    },

}
</script>

<style>

</style>