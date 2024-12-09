<template>
    <div class="ui-add-patient">
      <h2>{{ label }}</h2>
      <form @submit.prevent="submitForm">
        <div>
          <UIDropdown 
            :options="patients" 
            :select="'Выберите пациента'"
            :style="{marginBottom: '20px' }"
            @changed="setPatient"
          />
        </div>
        
        <div>
          <UIDropdown 
            :options="doctors" 
            :select="'Выберите доктора'"
            :style="{marginBottom: '20px' }"
            @changed="setDoctor"
          />
        </div>
        
        <div class="form-group">
          <label for="dob">Дата</label>
          <input type="date" :min="'1900-01-01'" :max="getMaxDate()" id="date" v-model="form.date" required />
        </div>
        
        <div>
          <UIDropdown 
            :options="getOptions()" 
            :select="'Не здоров'"
            @changed="setConclusion"
          />
        </div>
        
        <button class="ui-add-patient__button" type="submit">Сохранить</button>
      </form>
    </div>
</template>
  
<script>
import UIDropdown from "@/components/ui/dropdown/dropdown.vue"

const EXP = {
  0: "Healthy",
  1: "NotHealthy",
};

export default {
    name: "UIAddVisits",

    components: {
      UIDropdown,
    },

    props: {
        label: {
            type: String,
            default: "Добавить данные посещения",
        },
        patients: {
            type: Array,
        },
        doctors: {
            type: Array,
        },
    },

    data() {
      return {
        form: {
          patient: null,
          doctor: null,
          date: "",
          conclusion: null,
        },
      };
    },

    methods: {
      submitForm() {
        if (this.form.patient == null || this.form.doctor == null) {
          alert('Введите все данные');
        } else {
          const visit = {
            idPatient: this.form.patient,
            idDoctor: this.form.doctor,
            date: new Date(this.form.date).toISOString(),
            conclusion: this.form.conclusion ? this.form.conclusion : "NotHealthy",
          };

          this.$emit('form', visit);
        }
      },

      setConclusion(id) {
        this.form.conclusion = EXP[id];
      },

      setPatient(id) {
        this.form.patient = this.patients.find(p => p.id === id).id;
      },

      setDoctor(id) {
        this.form.doctor = this.doctors.find(d => d.id === id).id;
      },

      getMaxDate() {
        const date = new Date();
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, "0");
        const day = String(date.getDate()).padStart(2, "0");

        return `${year}-${month}-${day}`;
      },

      getOptions() {
        return [
          {
            id: 0,
            fullName: "Здоров",
          },
          {
            id: 1,
            fullName: "Не здоров",
          },          
        ]
      }
    },
};
</script>
