<template>
  <div class="patients">
    <PatientsContent 
        :patients="patients"
        @update="toggleUpdate" 
        @delete="clickDelete"
        @add="toggleAdd"
    />
  </div>
  <UIModel class="patients-model" v-if="add" @close="toggleAdd">
    <UIAddPatient @form="setPatient"/>
  </UIModel>
  <UIModel class="patients-model" v-if="update" @close="toggleUpdate">
    <UIUpdatePatient
        @form="updatePatient"
        :label="'Изменить пациента'"
        :patient="patients.find(p => p.id === id_update)"/>
  </UIModel>
</template>

<script>
import PatientsContent from "@/components/shared/patients/patients.vue"
import UIModel from "@/components/ui/model/model.vue"
import UIAddPatient from "@/components/ui/model/patient/add.vue"
import UIUpdatePatient from "@/components/ui/model/patient/update.vue"
import Api from "@/api/api";

const api = new Api();

export default {
    name: "Patients",

    components: {
        PatientsContent,
        UIModel,
        UIAddPatient,
        UIUpdatePatient,
    },

    data() {
        return {
            patients: [],
            add: false,
            update: false,
            id_update: null,
        }
    },

    async created() {
        this.getPatients();
    },

    methods: {
        async getPatients() {
            this.patients = await api.getPatients();
        },

        async setPatient(patient) {
            await api.addPatient(patient);
            this.getPatients();
            this.add = false;
        },

        async clickDelete(id) {
            await api.deletePatient(id);
            this.getPatients();
        },

        async updatePatient(patient) {
            await api.putPatient(this.id_update, patient);
            this.getPatients();
            this.update = false;
        },

        toggleUpdate(id) {
            this.update = !this.update; 
            this.id_update = id != null ? id : this.id_update;
        },

        toggleAdd() {
            this.add = !this.add;
        },
    },
}
</script>

<style lang="scss">
.patients {
    &-model {
        position: absolute;
        top: 0;
        left: 0;
    }
}
</style>