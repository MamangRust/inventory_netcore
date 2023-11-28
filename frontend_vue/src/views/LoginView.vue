<script setup>
import { useRoute } from 'vue-router';
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

import { router } from '@/helpers';
import { useAuthStore } from '@/stores';

const route = useRoute();
const authStore = useAuthStore();


if (authStore.user) {
    router.push('/');
}

const schema = Yup.object().shape({
    email: Yup.string().required('Email is required'),
    password: Yup.string().required('Password is required')
});

function onSubmit(values, { setErrors }) {
    const { email, password } = values;

    return authStore.login(email, password)
        .then(() => {
          
            router.push(route.query.returnUrl || '/');
        })
        .catch(error => setErrors({ apiError: error }));
}
</script>

<template>
    <div class="col-md-6 offset-md-3 mt-5">
       
        <h2>Login</h2>
        <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors, isSubmitting }">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <Field name="email" type="email" class="form-control" :class="{ 'is-invalid': errors.email }" />
                <div class="invalid-feedback">{{errors.email}}</div>
            </div>            
            <div class="mb-3">
                <label class="form-label">Password</label>
                <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
                <div class="invalid-feedback">{{errors.password}}</div>
            </div>            
            <div class="mb-3">
                <button class="btn btn-primary" :disabled="isSubmitting">
                    <span v-show="isSubmitting" class="spinner-border spinner-border-sm me-1"></span>
                    Login
                </button>
            </div>
            <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{errors.apiError}}</div>
        </Form>
    </div>
</template>