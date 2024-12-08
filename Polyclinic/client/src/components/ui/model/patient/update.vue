<template>
    <div class="ui-add-patient">
      <h2>{{ label }}</h2>
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label for="fullName">Полное имя</label>
          <input type="text" id="fullName" v-model="form.c_fullName" :placeholder="patient.fullName" required />
        </div>
        
        <div class="form-group">
          <label for="passport">Паспорт</label>
          <input type="text" id="passport" v-model="form.c_passport" :placeholder="patient.passport" required />
        </div>
        
        <div class="form-group">
          <label for="dob">Дата рождения</label>
          <input type="date" id="dob" v-model="form.c_dob" :placeholder="patient.dob" required />
        </div>
        
        <div class="form-group">
          <label for="address">Адрес</label>
          <input type="text" id="address" v-model="form.c_address" :placeholder="patient.adress" required />
        </div>
        
        <button class="ui-add-patient__button" type="submit">Сохранить</button>
      </form>
    </div>
</template>
  
<script>
export default {
    name: "UIUpdatePatient",

    props: {
        label: {
            type: String,
            default: "Изменить данные пациента",
        },
        patient: {
            type: Object,
        }
    },

    data() {
      return {
        form: {
          c_fullName: this.patient.fullName,
          c_passport: this.patient.passport,
          c_dob: this.patient.birth,
          c_address: this.patient.adress,
        },
      };
    },

    methods: {
      submitForm() {
        const patient = {
          passport: this.form.c_passport,
          fullName: this.form.c_fullName,
          birth: new Date(this.form.c_dob).toISOString(),
          adress: this.form.c_address,
        };

        this.$emit('form', patient);
        console.log("Данные пациента:", patient);
      },
    },
};
</script>

<!-- <style lang="scss">
.ui-add-patient {
    max-width: 500px;
    margin: auto;

    h2 {
        font-size: 24px;
        font-family: 'Montserrat-Medium';
        color: black;
        text-align: center;
    }

    &__button {
        width: 100%;
        padding: 10px;
        background-color: #7D6970;
        color: white;
        font-size: 16px;
        border-radius: 10px;
        border: 2px solid #7D6970;
        margin-top: 40px;

        &:hover {
            background-color: white;
            color: #7D6970;
        }
    }
}


.form-group {
    font-size: 18px;
    font-family: 'Montserrat-Light';
    color: black;
    text-align: start;
    margin-bottom: 20px;
}

input {
    width: 100%;
    padding: 8px;
    font-size: 14px;
    border: 1px solid #7D6970;
    border-radius: 4px;
}

::placeholder {
    font-size: 12px;
    font-family: 'Montserrat-Light';
    color: black;
    text-align: start;  
}
</style>
 -->

