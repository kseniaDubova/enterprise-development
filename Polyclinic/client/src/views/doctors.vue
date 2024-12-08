<template>
    <div class="doctors">
      <DoctorsContent 
          :doctors="doctors"
          @update="toggleUpdate" 
          @delete="clickDelete"
          @add="toggleAdd"
      />
    </div>
    <UIModel class="doctors-model" v-if="add" @close="toggleAdd">
      <UIAddDoctor @form="setDoctor"/>
    </UIModel>
    <UIModel class="doctors-model" v-if="update" @close="toggleUpdate">
      <UIUpdateDoctor 
        @form="updateDoctor"
        :doctor="doctors.find(d => d.id === id_update)"/>
    </UIModel>
</template>

<script>
import DoctorsContent from "@/components/shared/doctors/doctors.vue"
import UIModel from "@/components/ui/model/model.vue"
import UIAddDoctor from "@/components/ui/model/doctor/add.vue"
import UIUpdateDoctor from "@/components/ui/model/doctor/update.vue"
import Api from "@/api/api";

const api = new Api();

export default {
    name: "Doctors",

    components: {
        DoctorsContent,
        UIModel,
        UIAddDoctor,
        UIUpdateDoctor,
    },

    data() {
        return {
            doctors: [],
            add: false,
            update: false,
            id_update: null,
        }
    },

    async created() {
        this.getDoctors();
    },

    methods: {
        async getDoctors() {
            this.doctors = await api.getDoctors();
        },

        async setDoctor(doctor) {
            await api.addDoctor(doctor);
            console.log(doctor);
        },

        async clickDelete(id) {
            await api.deleteDoctor(id)
        },

        async updateDoctor(doctor) {
            await api.putDoctor(this.id_update, doctor);
        },

        toggleUpdate(id) {
            this.update = !this.update; 
            this.id_update = id != null ? id : this.id_update;
        },

        toggleAdd() {
            this.add = !this.add;
            console.log(this.add)
        },
    },
}
</script>

<style lang="scss">
.doctors {
    &-model {
        position: absolute;
        top: 0;
        left: 0;
    }
}
</style>