<template>
    <div class="ui-add-patient">
      <h2>{{ label }}</h2>
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label for="fullName">Полное имя</label>
          <input type="text" id="fullName" v-model="form.c_fullName" :placeholder="doctor.fullName" required />
        </div>
        
        <div class="form-group">
          <label for="passport">Паспорт</label>
          <input type="text" id="passport" v-model="form.c_passport" :placeholder="doctor.passport" required />
        </div>
        
        <div class="form-group">
          <label for="dob">Дата рождения</label>
          <input type="date" id="dob" v-model="form.c_dob" :placeholder="doctor.birth" required />
        </div>
        
        <div class="form-group">
          <label for="experience">Опыт</label>
          <input type="number" id="experience" v-model="form.c_experience" :placeholder="doctor.experience" required />
        </div>

        <div>
          <UIDropdown 
            :options="getOptions()" 
            :select="doctor.specialization"
            @changed="setSpecialization"
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
    name: "UIUpdateDoctor",

    components: {
      UIDropdown,
    },

    props: {
        label: {
            type: String,
            default: "Изменить данные доктора",
        },
        doctor: {
            type: Object,
        }
    },

    data() {
      return {
        form: {
          c_fullName: this.doctor.fullName,
          c_passport: this.doctor.passport,
          c_dob: this.doctor.birth,
          c_specialization: this.doctor.specialization,
          c_experience: this.doctor.experience,
        },
      };
    },

    methods: {
      submitForm() {
        const doctor = {
          passport: this.form.c_passport,
          fullName: this.form.c_fullName,
          birth: new Date(this.form.c_dob).toISOString(),
          specialization: this.form.c_specialization,
          experience: this.form.c_experience,
        };

        this.$emit('form', doctor);
        console.log("Данные доктора:", doctor);
      },

      setSpecialization(id) {
        console.log(EXP[id])
        this.form.c_specialization = EXP[id];
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
