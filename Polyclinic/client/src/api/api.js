import http from "@/api/http.js";

export default class Api {
    static EXP = {
        0: "Терапевт",
        1: "Хирург",
        2: "Дантист",
        3: "Вирусолог",
        4: "Дерматолог",
    };
    /// GET ///

    async getPatients() {
        const { data } = await http.get("/Patient");

        for (let p of data) {
            p.birth = p.birth.split("T")[0];
        }

        return data;
    }

    async getDoctors() {
        const { data } = await http.get("/Doctor");
        const doctors = [];

        for (let d of data) {
            const tmp = {
                id: d.id,
                passport: d.passport,
                fullName: d.fullName,
                birth: d.birth.split("T")[0],
                experience: d.experience,
                specialization: Api.EXP[d.specialization],
            }
            doctors.push(tmp);
        }
        return doctors;
    }

    async getVisits() {
        const { data } = await http.get("/Appointment");
        const visits = [];

        const EXP_s = {
            0: "Здоров",
            1: "Не здоров",
        };

        for (let v of data) {
            const tmp = {
                id: v.id,
                patient: v.patient.fullName,
                doctor: v.doctor.fullName,
                date: v.date.split("T")[0],
                conclusion: EXP_s[v.conclusion],
            }
            visits.push(tmp);
        }

        return visits;
    }

    /// GET REQUESTS ///

    async getExperienceDoctors() {
        const { data } = await http.get("/Requests/experience-doctors");
        const res = [];

        for (let d of data) {
            const tmp = {
                id: d.id,
                passport: d.passport,
                fullName: d.fullName,
                birth: d.birth.split("T")[0],
                experience: d.experience,
                specialization: Api.EXP[d.specialization],
            }
            res.push(tmp);
        }
        console.log(res)

        return res;
    }

    async getPatientsOfDoctors(id) {
        const { data } = await http.get(`/Requests/patients-of-doctor/${id}`);

        for (let p of data) {
            p.birth = p.birth.split("T")[0];
        }

        return data;
    }

    async getHealthyPatients() {
        const { data } = await http.get("/Requests/healthy-patients");

        for (let p of data) {
            p.birth = p.birth.split("T")[0];
        }

        return data;
    }

    async getCountAppointment() {
        const { data } = await http.get("/Requests/count-appointment");
        const doctors = await this.getDoctors();
        const res = [];

        for (let d of data) {
            const tmp = {
                doctor: doctors.find(i => i.id === d.doctor).fullName,
                count: d.count,
            }
            res.push(tmp);
        }

        return res;
    }

    async getDiseaseTop() {
        const { data } = await http.get("/Requests/disease-top");
        const res = [];

        for (let d of data) {
            const tmp = {
                specialization: Api.EXP[d.specialization],
                top: d.count,
            }
            res.push(tmp);
        }

        return res;
    }

    async getPatientWithSeveralAppointment() {
        const { data } = await http.get("/Requests/patients-with-several-appointment");

        for (let p of data) {
            p.birth = p.birth.split("T")[0];
        }

        return data;
    }

    /// POST ///

    async addPatient(patient) {
        const { data } = await http.post('/Patient', patient);
        return data.data;
    }

    async addDoctor(doctor) {
        const { data } = await http.post('/Doctor', doctor);
        return data.data;
    }

    async addVisit(visit) {
        const { data } = await http.post('/Appointment', visit);
        return data.data;
    }

    /// DELETE ///

    async deletePatient(id) {
        const { data } = await http.delete(`/Patient/${id}`);
        return data.data;
    }

    async deleteDoctor(id) {
        const { data } = await http.delete(`/Doctor/${id}`);
        return data.data;
    }

    async deleteVisit(id) {
        const { data } = await http.delete(`/Appointment/${id}`);
        return data.data;
    }

    /// PUT ///

    async putPatient(id, patient) {
        const { data } = await http.put(`/Patient/${id}`, patient);
        return data.data;
    }

    async putDoctor(id, doctor) {
        const { data } = await http.put(`/Doctor/${id}`, doctor);
        return data.data;
    }

    async putVisit(id, visit) {
        const { data } = await http.put(`/Appointment/${id}`, visit);
        return data.data;
    }
}