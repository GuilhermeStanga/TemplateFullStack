import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import { VSnackbar } from 'vuetify/lib'
import VuetifyToast from 'vuetify-toast-snackbar-ng'

Vue.use(Vuetify, {
    components: {
      VSnackbar
    }
  })

Vue.use(VuetifyToast)

export default new Vuetify({
});
