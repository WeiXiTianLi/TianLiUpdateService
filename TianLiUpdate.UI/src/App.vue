<template>
  <div id="app">
    <ProjectPage msg="12" :items="[{id:'asd',name:'sdf'},{id:'asdasd',name:'sdff'}]"></ProjectPage>
  </div>
</template>

<script>
import ProjectPage from './components/ProjectPage.vue';

export default {
  name: 'App',
  components: {
    ProjectPage,
  },

};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
