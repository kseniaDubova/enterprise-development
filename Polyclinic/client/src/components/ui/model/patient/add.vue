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
          <input type="date" id="dob" :min="'1900-01-01'" :max="getMaxDate()" v-model="form.dob" required />
        </div>
        
        <div class="form-group">
          <label for="address">Адрес</label>
          <input type="text" id="address" v-model="form.address" placeholder="Введите адрес" required />
        </div>
        
        <button class="ui-add-patient__button" type="submit">Сохранить</button>
      </form>
    </div>
</template>
  
<script>
export default {
    name: "UIAddPatient",

    props: {
        label: {
            type: String,
            default: "Добавить данные пациента",
        }
    },

    data() {
      return {
        form: {
          fullName: "",
          passport: "",
          dob: "",
          address: "",
        },
      };
    },

    methods: {
      submitForm() {
        const patient = {
          passport: this.form.passport,
          fullName: this.form.fullName,
          birth: new Date(this.form.dob).toISOString(),
          adress: this.form.address,
        };

        this.$emit('form', patient);
      },

      getMaxDate() {
        const date = new Date();
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, "0");
        const day = String(date.getDate()).padStart(2, "0");

        return `${year}-${month}-${day}`;
      }
    },
};
</script>

<style lang="scss">
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