<template>
    <div class="visits">
      <VisitsContent 
          :visits="visits"
          @update="toggleUpdate" 
          @delete="clickDelete"
          @add="toggleAdd"
      />
    </div>
    <UIModel class="visits-model" v-if="add" @close="toggleAdd">
      <UIAddVisits @form="setVisit" :doctors="doctors" :patients="patients"/>
    </UIModel>
    <UIModel class="visits-model" v-if="update" @close="toggleUpdate">
      <UIDropdown 
        :doctors="doctors" 
        :patients="patients" 
        :visit="visits.find(v => v.id === id_update)"
        @form="updateVisit"/>
    </UIModel>
</template>

<script>
import VisitsContent from "@/components/shared/visits/visits.vue"
import UIModel from "@/components/ui/model/model.vue"
import UIAddVisits from "@/components/ui/model/visit/add.vue"
import UIDropdown from "@/components/ui/model/visit/update.vue"
import Api from "@/api/api";

const api = new Api();

export default {
    name: "Visits",

    components: {
        VisitsContent,
        UIModel,
        UIAddVisits,
        UIDropdown,
    },

    data() {
        return {
            visits: [],
            patients: [],
            doctors: [],
            add: false,
            update: false,
            id_update: null,
        }
    },

    async created() {
        this.getVisits();
        this.getPatients();
        this.getDoctors();
    },

    methods: {
        async getVisits() {
            this.visits = await api.getVisits();
        },

        async getPatients() {
            this.patients = await api.getPatients();
        },

        async getDoctors() {
            this.doctors = await api.getDoctors();
        },

        async setVisit(visits) {
            await api.addVisit(visits);
            console.log(visits);
        },

        async clickDelete(id) {
            await api.deleteVisit(id);
        },

        async updateVisit(visit) {
            await api.putVisit(this.id_update, visit);
        },

        toggleUpdate(id) {
            this.update = !this.update; 
            this.id_update = id != null ? id : this.id_update;
            console.log(this.id_update)
        },

        toggleAdd() {
            this.add = !this.add;
            console.log(this.add)
        },
    },
}
</script>

<style lang="scss">
.visits {
    &-model {
        position: absolute;
        top: 0;
        left: 0;
    }
}
</style>