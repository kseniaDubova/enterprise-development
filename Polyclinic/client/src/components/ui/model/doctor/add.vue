<template>
    <div class="ui-add-patient">
      <h2>{{ label }}</h2>
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label for="fullName">Полное имя</label>
          <input type="text" id="fullName" v-model="form.fullName" placeholder="Введите полное имя" required />
        </div>
        
        <div class="form-group">
          <label for="passport">Паспорт</label>
          <input type="text" id="passport" v-model="form.passport" placeholder="Введите паспортные данные" required />
        </div>
        
        <div class="form-group">
          <label for="dob">Дата рождения</label>
          <input type="date" id="dob" v-model="form.dob" required />
        </div>
        
        <div class="form-group">
          <label for="experience">Опыт</label>
          <input type="number" id="experience" v-model="form.experience" placeholder="Введите опыт в годах" required />
        </div>

        <div>
          <UIDropdown 
            :options="getOptions()" 
            :select="'Терапевт'"
            @changed="setSpetialization"
          />
        </div>
        
        <button class="ui-add-patient__button" type="submit">Сохранить</button>
      </form>
    </div>
</template>
  
<script>
import UIDropdown from "@/components/ui/dropdown/dropdown.vue"

const EXP = {
  0: "Therapist",
  1: "Surgeon",
  2: "Dentist",
  3: "Virologist",
  4: "Dermatologist",
};

export default {
    name: "UIAddDoctor",

    components: {
      UIDropdown,
    },

    props: {
        label: {
            type: String,
            default: "Добавить данные доктора",
        }
    },

    data() {
      return {
        form: {
          fullName: "",
          passport: "",
          dob: "",
          experience: null,
          specialization: null,
        },
      };
    },

    methods: {
      submitForm() {
        const doctor = {
          passport: this.form.passport,
          fullName: this.form.fullName,
          birth: new Date(this.form.dob).toISOString(),
          specialization: this.form.specialization ? this.form.specialization : "Therapist",
          experience: this.form.experience,
        };

        this.$emit('form', doctor);
      },

      setSpetialization(id) {
        this.form.specialization = EXP[id];
      },

      getOptions() {
        return [
          {
            id: 0,
            fullName: "Терапевт",
          },
          {
            id: 1,
            fullName: "Хирург",
          },          
          {
            id: 2,
            fullName: "Дантист",
          },          
          {
            id: 3,
            fullName: "Вирусолог",
          },          
          {
            id: 4,
            fullName: "Дерматолог",
          },
        ]
      }
    },
};
</script>
