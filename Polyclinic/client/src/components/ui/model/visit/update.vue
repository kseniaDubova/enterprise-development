<template>
    <div class="ui-add-patient">
      <h2>{{ label }}</h2>
      <form @submit.prevent="submitForm">
        <div>
          <UIDropdown 
            :options="patients" 
            :select="form.c_patient"
            :style="{marginBottom: '20px' }"
            @changed="setPatient"
          />
        </div>
        
        <div>
          <UIDropdown 
            :options="doctors" 
            :select="form.c_doctor"
            :style="{marginBottom: '20px' }"
            @changed="setDoctor"
          />
        </div>
        
        <div class="form-group">
          <label for="date">Дата рождения</label>
          <input type="date" id="date" v-model="form.c_date" :placeholder="form.c_date" required />
        </div>
        
        <div>
          <UIDropdown 
            :options="getOptions()" 
            :select="form.c_conclusion"
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
    name: "UIUpdateVisit",

    components: {
      UIDropdown,
    },

    props: {
        label: {
            type: String,
            default: "Изменить данные посещения",
        },
        visit: {
            type: Object,
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
          c_patient: this.visit.patient,
          c_doctor: this.visit.doctor,
          c_date: this.visit.date,
          c_conclusion: this.visit.conclusion,
        },
      };
    },

    methods: {
      submitForm() {
        const visit = {
          patient: this.form.c_patient,
          doctor: this.form.c_doctor,
          date: new Date(this.form.c_date).toISOString(),
          conclusion: this.form.c_conclusion,
        };

        this.$emit('form', visit);
        console.log("Данные посещения:", visit);
      },

      setConclusion(id) {
        this.form.c_conclusion = EXP[id];
      },

      setPatient(id) {
        this.form.c_patient = this.patients.find(p => p.id === id).id;
      },

      setDoctor(id) {
        this.form.c_doctor = this.doctors.find(d => d.id === id).id
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
